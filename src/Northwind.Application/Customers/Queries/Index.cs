namespace Northwind.Application.Customers.Queries
{
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using AutoMapper.QueryableExtensions;
  using Common.Mappings;
  using Domain;
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
      
      public class Item : IMapFrom<Customer>
      {
        public string Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
          profile.CreateMap<Customer, Item>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CustomerId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CompanyName));
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
        var items = await _db.Customers
          .ProjectTo<Model.Item>(_mapper.ConfigurationProvider)
          .ToListAsync(token);
    
        var model = new Model
        {
          Items = items
        };
    
        return model;
        
      }
      
    }
    
  }
  
}