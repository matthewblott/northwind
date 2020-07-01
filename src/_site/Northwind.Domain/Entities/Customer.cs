namespace Northwind.Domain.Entities
{
  using System.Collections.Generic;
  using FluentValidation;
  using FluentValidation.Validators;

  public class Customer
  {
    public ICollection<Order> Orders { get; private set; }
    public Customer() => Orders = new HashSet<Order>();

    public string CustomerId { get; set; }
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

    // Validator
    public class Validator : AbstractValidator<Customer>
    {
      public Validator()
      {
        RuleFor(x => x.CustomerId)
          .MinimumLength(3)
          .MaximumLength(5)
          .Matches("[A-Z]")
          .NotEmpty();

        RuleFor(x => x.Address).MaximumLength(60);
        RuleFor(x => x.City).MaximumLength(15);
        RuleFor(x => x.CompanyName).MaximumLength(40).NotEmpty();
        RuleFor(x => x.ContactName).MaximumLength(30);
        RuleFor(x => x.ContactTitle).MaximumLength(30);
        RuleFor(x => x.Country).MaximumLength(15);
        RuleFor(x => x.Fax).MaximumLength(24);
        RuleFor(x => x.Phone).MaximumLength(24);
        RuleFor(x => x.PostalCode).MaximumLength(10);
        RuleFor(x => x.Region).MaximumLength(15);

        // RuleFor(c => c.PostalCode).Matches(@"^\d{4}$")
        //   .When(c => c.Country == "Australia")
        //   .WithMessage("Australian Postcodes have 4 digits");
        //
        // RuleFor(c => c.Phone)
        //   .Must(HaveQueenslandLandLine)
        //   .When(c => c.Country == "Australia" && c.PostalCode.StartsWith("4"))
        //   .WithMessage("Customers in QLD require at least one QLD landline.");
        
      }

      private static bool HaveQueenslandLandLine(Customer model, string phoneValue, PropertyValidatorContext ctx) 
        => model.Phone.StartsWith("07") || model.Fax.StartsWith("07");
      
    }
    
  }
  
}