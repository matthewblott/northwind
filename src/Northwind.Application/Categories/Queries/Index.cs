namespace Northwind.Application.Categories.Queries
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
      
      public class Item : IMapFrom<Category>
      {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
          profile.CreateMap<Category, Item>()
            .ForMember(d => d.Id,opt
              => opt.MapFrom(s => s.CategoryId))
            .ForMember(d => d.CategoryName, opt
                => opt.MapFrom(s => s.CategoryName))
            .ForMember(d => d.Description,opt
              => opt.MapFrom(s => s.Description));
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
        var items = await _db.Categories
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