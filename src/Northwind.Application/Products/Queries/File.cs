namespace Northwind.Application.Products.Queries
{
  using AutoMapper;
  using Common.Interfaces;
  using Domain.Entities;
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper.QueryableExtensions;
  using Common.Mappings;
  using CsvHelper.Configuration;
  using MediatR;
  using Microsoft.EntityFrameworkCore;
  using Northwind.Common;

  public class File
  {
    public sealed class ProductFileRecordMap : ClassMap<Item>
    {
      public ProductFileRecordMap()
      {
        //AutoMap();
        Map(m => m.UnitPrice).Name("Unit Price").ConvertUsing(c => (c.UnitPrice ?? 0).ToString("C"));
      }
    }
    
    public class Query : IRequest<Model>
    {
    }
    
    public class Model
    {
      public string FileName { get; set; }
      public string ContentType { get; set; }
      public byte[] Content { get; set; }
    }
    
    public class Item : IMapFrom<Product>
    {
      public string Category { get; set; }

      public string Name { get; set; }

      public decimal? UnitPrice { get; set; }

      public bool Discontinued { get; set; }

      public void Mapping(Profile profile)
      {
        profile.CreateMap<Product, Item>()
          .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ProductName))
          .ForMember(d => d.Category,
            opt => opt.MapFrom(s => s.Category != null ? s.Category.CategoryName : string.Empty));
      }
    }
    
    public class Handler : IRequestHandler<Query, Model>
    {
      private readonly INorthwindDbContext _context;
      private readonly IMapper _mapper;
      private readonly IDateTime _dateTime;

      public Handler(INorthwindDbContext context, IMapper mapper, IDateTime dateTime)
      {
        _context = context;
        _mapper = mapper;
        _dateTime = dateTime;
      }

      public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
      {
        var records = await _context.Products
          .ProjectTo<Item>(_mapper.ConfigurationProvider)
          .ToListAsync(cancellationToken);

        // var fileContent = _fileBuilder.BuildProductsFile(records);

        var vm = new Model
        {
          // Content = fileContent,
          ContentType = "text/csv",
          FileName = $"{_dateTime.Now:yyyy-MM-dd}-Products.csv"
        };

        return vm;
      }
    }

  }
  
}