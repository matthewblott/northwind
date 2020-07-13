namespace Northwind.Application.Products.Queries
{
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using AutoMapper.QueryableExtensions;
  using Common.Interfaces;
  using Common.Mappings;
  using Domain.Entities;
  using MediatR;
  using Microsoft.EntityFrameworkCore;

  public class Index
  {
    public class Query : IRequest<Model>
    {
    }

    public class Model
    {
      public IList<Item> Items { get; set; }
      
      public bool CreateEnabled { get; set; }
      
      public class Item : IMapFrom<Product>
      {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierCompanyName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool Discontinued { get; set; }

        public void Mapping(Profile profile)
        {
          profile.CreateMap<Product, Item>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductId))
            .ForMember(d => d.SupplierCompanyName,
              opt => opt.MapFrom(s => s.Supplier != null ? s.Supplier.CompanyName : string.Empty))
            .ForMember(d => d.CategoryName,
              opt => opt.MapFrom(s => s.Category != null ? s.Category.CategoryName : string.Empty));
        }
        
      }
      
    }
    
    public class Handler : IRequestHandler<Query, Model>
    {
      private readonly INorthwindDbContext _db;
      private readonly IMapper _mapper;
    
      public Handler(INorthwindDbContext db, IMapper mapper)
      {
        _db = db;
        _mapper = mapper;
      }
    
      public async Task<Model> Handle(Query query, CancellationToken token)
      {
        var items = await _db.Products
          .ProjectTo<Model.Item>(_mapper.ConfigurationProvider)
          .ToListAsync(token);
    
        var model = new Model
        {
          Items = items,
          CreateEnabled = true // TODO: Set based on user permissions.
        };
    
        return model;
        
      }
      
    }
    
  }

}