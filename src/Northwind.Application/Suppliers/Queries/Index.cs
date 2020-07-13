namespace Northwind.Application.Suppliers.Queries
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
        public string Fax { get; set; }
        public string HomePage { get; set; }

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
          .ToListAsync(token);
    
        var model = new Model
        {
          Items = items,
          // CreateEnabled = true // TODO: Set based on user permissions.
        };
    
        return model;
        
      }
      
    }
    
  }
}