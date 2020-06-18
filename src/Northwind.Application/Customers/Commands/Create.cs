namespace Northwind.Application.Customers.Commands
{
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using Common.Interfaces;
  using Domain.Entities;
  using FluentValidation;
  using MediatR;

  public class Create
  {
    public class Command : IRequest<Unit>
    {
      [IgnoreMap]
      public string Id { get; set; }
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
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.Id).Length(5).NotEmpty();
        RuleFor(x => x.Address).MaximumLength(60);
        RuleFor(x => x.City).MaximumLength(15);
        RuleFor(x => x.CompanyName).MaximumLength(40).NotEmpty();
        RuleFor(x => x.ContactName).MaximumLength(30);
        RuleFor(x => x.ContactTitle).MaximumLength(30);
        RuleFor(x => x.Country).MaximumLength(15);
        RuleFor(x => x.Fax).MaximumLength(24);
        RuleFor(x => x.Phone).MaximumLength(24);
        RuleFor(x => x.PostalCode).MaximumLength(10).NotEmpty();
        RuleFor(x => x.Region).MaximumLength(15);
      }
    }
    
    public class Handler : IRequestHandler<Command, Unit>
    {
      private readonly INorthwindDbContext _db;
      private readonly IMapper _mapper;

      public Handler(INorthwindDbContext db, IMapper mapper)
      {
        _db = db;
        _mapper = mapper;
      }

      public async Task<Unit> Handle(Command command, CancellationToken token)
      {
        var entity = new Customer
        {
          CustomerId = command.Id.ToUpper(),
          CompanyName = command.CompanyName,
          ContactName = command.ContactName,
          ContactTitle = command.ContactTitle,
          Phone = command.Phone,
          Fax = command.Fax,
        };
        
        await _db.Customers.AddAsync(entity, token);
        await _db.SaveChangesAsync(token);

        return Unit.Value;

      }
      
    }
    
  }
  
}