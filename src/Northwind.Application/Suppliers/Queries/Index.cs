namespace Northwind.Application.Suppliers.Queries
{
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using AutoMapper.QueryableExtensions;
  using Common;
  using Common.Interfaces;
  using Common.Mappings;
  using Domain.Entities;
  using MediatR;
  using Microsoft.EntityFrameworkCore;
  using X.PagedList;

  public class Index
  {
    public class Query : IRequest<Model>
    {
      public int Page { get; set; } = 1;
    }

    public class Model
    {
      public IPagedList<Item> Items { get; set; }
      
      public bool CreateEnabled { get; set; }
      
      public class Item : IMapFrom<Supplier>
      {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string? Fax { get; set; }
        public string? HomePage { get; set; }

        public void Mapping(Profile profile)
        {
          profile.CreateMap<Supplier, Item>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierId));
          
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
        var items = await _db.Suppliers
          .ProjectTo<Model.Item>(_mapper.ConfigurationProvider)
          .ToPagedListAsync(query.Page, PageConstants.PageSize, token);
        
        var model = new Model
        {
          Items = items,
        };
    
        return model;
        
      }
      
    }
    
  }
}