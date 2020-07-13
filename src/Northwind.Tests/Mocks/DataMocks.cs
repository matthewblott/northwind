namespace Northwind.Tests.Mocks
{
  using System.Collections.Generic;
  using Bogus;
  using Bogus.DataSets;
  using Domain.Entities;

  public static class DataMocks
  {
    public static IEnumerable<Customer> GetCustomerList(int count)
    {
      var generator = new Faker<Customer>()
          .RuleFor(c => c.CustomerId, f => (f.IndexFaker + 1)
            .ToString("00000"))
          .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
          .RuleFor(c => c.ContactName, f =>
            f.Name.FirstName(f.PickRandom<Name.Gender>()))
        ;

      var customers = new List<Customer>
      {
        // new Faker<Customer>()
        //   .RuleFor(c => c.CustomerId, f => "")
        //   .RuleFor(c => c.CompanyName, f => "")
        //   .RuleFor(c => c.ContactName, f => "")
        //   .RuleFor(c => c.ContactTitle, f => "")
        //   .RuleFor(c => c.Address, f => "")
        //   .RuleFor(c => c.City, f => "")
        //   .RuleFor(c => c.Region, f => "")
        //   .RuleFor(c => c.PostalCode, f => "")
        //   .RuleFor(c => c.Country, f => "")
        //   .RuleFor(c => c.Phone, f => "")
        //   .RuleFor(c => c.Fax, f => ""),

        new Faker<Customer>()
          .RuleFor(c => c.CustomerId, f => "ALFKI")
          .RuleFor(c => c.CompanyName, f => "Alfreds Futterkiste")
          .RuleFor(c => c.ContactName, f => "Maria Anders")
          .RuleFor(c => c.ContactTitle, f => "Sales Representative")
          .RuleFor(c => c.Address, f => "Obere Str. 57")
          .RuleFor(c => c.City, f => "Berlin")
          .RuleFor(c => c.Region, f => "Western Europe")
          .RuleFor(c => c.PostalCode, f => "12209")
          .RuleFor(c => c.Country, f => "Germany")
          .RuleFor(c => c.Phone, f => "030-0074321")
          .RuleFor(c => c.Fax, f => "030-0076545"),
        new Faker<Customer>()
          .RuleFor(c => c.CustomerId, f => "ANATR")
          .RuleFor(c => c.CompanyName, f => "Ana Trujillo Emparedados y helados")
          .RuleFor(c => c.ContactName, f => "Ana Trujillo")
          .RuleFor(c => c.ContactTitle, f => "Owner")
          .RuleFor(c => c.Address, f => "Avda. de la Constitución 2222")
          .RuleFor(c => c.City, f => "México D.F.")
          .RuleFor(c => c.Region, f => "Central America")
          .RuleFor(c => c.PostalCode, f => "05021")
          .RuleFor(c => c.Country, f => "Mexico")
          .RuleFor(c => c.Phone, f => "(5) 555-4729")
          .RuleFor(c => c.Fax, f => "(5) 555-3745"),
      };

      for (var i = 0; i < count; i++)
      {
        customers.Add(generator.Generate());
      }

      return customers;
    }
    
  }
  
}