namespace Northwind.Application.Customers.Queries
{
  using System.ComponentModel.DataAnnotations;
  using AutoMapper;
  using Common.Interfaces;
  using Domain.Entities;
  using System.Threading;
  using System.Threading.Tasks;
  using Common.Mappings;
  using FluentValidation;
  using MediatR;

  public class Details
  {
    public class Query : IRequest<Model>
    {
      public string Id { get; set; }
    }
    
    public class Validator : AbstractValidator<Query>
    {
      public Validator()
      {
        RuleFor(v => v.Id).NotEmpty().Length(5);
      }
    }

    public class Model : IMapFrom<Customer>
    {
      public string Id { get; set; }
      
      [Display(Name = "Company")]
      public string CompanyName { get; set; }
      [Display(Name = "Contact Title")]
      public string ContactTitle { get; set; }
      [Display(Name = "Contact Name")]
      public string ContactName { get; set; }
      public string Phone { get; set; }
      public string Fax { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      public string PostalCode { get; set; }
      public string Country { get; set; }
      public string Region { get; set; }

      public void Mapping(Profile profile)
      {
        profile.CreateMap<Customer, Model>()
          .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CustomerId));
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
        var id = query.Id.ToUpper();
        var entity = await _db.Customers.FindAsync(id);

        return _mapper.Map<Model>(entity);
      }
      
    }
    
  }
  
}