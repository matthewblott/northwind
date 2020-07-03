namespace Northwind.Application.Employees.Queries
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
      
      public class Item : IMapFrom<Employee>
      {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
          profile.CreateMap<Employee, Item>()
            .ForMember(d => d.Id,opt
              => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.FirstName, opt
                => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName,opt
              => opt.MapFrom(s => s.LastName));
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
        var items = await _db.Employees
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