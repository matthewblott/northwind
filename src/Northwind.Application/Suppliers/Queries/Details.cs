namespace Northwind.Application.Suppliers.Queries
{
  using AutoMapper;
  using Domain.Entities;
  using System.Threading;
  using System.Threading.Tasks;
  using Common.Interfaces;
  using Common.Mappings;
  using FluentValidation;
  using MediatR;

  public class Details
  {
    public class Query : IRequest<Model>
    {
      public int Id { get; set; }
    }
    
    public class Validator : AbstractValidator<Query>
    {
      public Validator()
      {
      }
    }

    public class Model : IMapFrom<Supplier>
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
        profile.CreateMap<Supplier, Model>()
          .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierId));
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
        var id = query.Id;
        var entity = await _db.Suppliers.FindAsync(id);

        return _mapper.Map<Model>(entity);
      }
      
    }
    
  }
  
}