namespace Northwind.Application.Customers.Queries
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
          .ToPagedListAsync(query.Page, PageConstants.PageSize, token);
    
        var model = new Model
        {
          Items = items
        };
    
        return model;
        
      }
      
    }
    
  }
  
}