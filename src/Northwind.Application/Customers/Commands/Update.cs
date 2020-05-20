namespace Northwind.Application.Customers.Commands
{
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using Common.Interfaces;
  using FluentValidation;
  using FluentValidation.Validators;
  using MediatR;
  using Microsoft.EntityFrameworkCore;

  public class Update
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

    // Validator
    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.Id).MaximumLength(5).NotEmpty();
        RuleFor(x => x.Address).MaximumLength(60);
        RuleFor(x => x.City).MaximumLength(15);
        RuleFor(x => x.CompanyName).MaximumLength(40).NotEmpty();
        RuleFor(x => x.ContactName).MaximumLength(30);
        RuleFor(x => x.ContactTitle).MaximumLength(30);
        RuleFor(x => x.Country).MaximumLength(15);
        RuleFor(x => x.Fax).MaximumLength(24).NotEmpty();
        RuleFor(x => x.Phone).MaximumLength(24).NotEmpty();
        RuleFor(x => x.PostalCode).MaximumLength(10);
        RuleFor(x => x.Region).MaximumLength(15);

        RuleFor(c => c.PostalCode).Matches(@"^\d{4}$")
          .When(c => c.Country == "Australia")
          .WithMessage("Australian Postcodes have 4 digits");

        RuleFor(c => c.Phone)
          .Must(HaveQueenslandLandLine)
          .When(c => c.Country == "Australia" && c.PostalCode.StartsWith("4"))
          .WithMessage("Customers in QLD require at least one QLD landline.");
      }

      private static bool HaveQueenslandLandLine(Command model, string phoneValue, PropertyValidatorContext ctx) 
        => model.Phone.StartsWith("07") || model.Fax.StartsWith("07");
    }
    
    // Handler
    public class Handler : IRequestHandler<Command>
    {
      private readonly INorthwindDbContext _context;

      public Handler(INorthwindDbContext context)
      {
        _context = context;
      }

      public async Task<Unit> Handle(Command command, CancellationToken token)
      {
        var entity = await _context.Customers
          .SingleOrDefaultAsync(c => c.CustomerId == command.Id.ToUpper(), token);

        entity.Address = command.Address;
        entity.City = command.City;
        entity.CompanyName = command.CompanyName;
        entity.ContactName = command.ContactName;
        entity.ContactTitle = command.ContactTitle;
        entity.Country = command.Country;
        entity.Fax = command.Fax;
        entity.Phone = command.Phone;
        entity.PostalCode = command.PostalCode;

        await _context.SaveChangesAsync(token);

        return Unit.Value;
        
      }
      
    }

  }
  
}