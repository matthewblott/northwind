namespace Northwind.Tests.Mocks
{
  using System.Collections.Generic;
  using Bogus;
  using Bogus.DataSets;
  using Domain.Entities;

  public class CustomerMock
  {
    public CustomerMock()
    {
      var customers = new Faker<Customer>()
        .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
        .RuleFor(c => c.CustomerId, f => 
          f.Company.CompanyName().Substring(0, 5).ToUpper())
        .RuleFor(c => c.ContactName, f =>
          f.Name.FirstName(f.PickRandom<Name.Gender>()))
        ;
    }

    public static IList<Customer> ListOf50
    {
      get
      {
        var customerGenerator = new Faker<Customer>()
            .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
            .RuleFor(c => c.CustomerId, f => 
              f.Company.CompanyName().Substring(0, 5).ToUpper())
            .RuleFor(c => c.ContactName, f =>
              f.Name.FirstName(f.PickRandom<Name.Gender>()))
          ;

        IList<Customer> newCustomers = new List<Customer>();

        for (var i = 0; i < 10; i++)
        {
          newCustomers.Add(customerGenerator.Generate());
        }
        
        return newCustomers;

      }
      
    }
    
  }
  
}