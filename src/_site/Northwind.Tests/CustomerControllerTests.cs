namespace Northwind.Tests
{
  using Application.Customers.Commands;
  using AutoFixture.NUnit3;
  using MyTested.AspNetCore.Mvc;
  using NUnit.Framework;
  using WebUI.Features.Customers;
  using HttpMethod = System.Net.Http.HttpMethod;

  public class CustomerControllerTests
  {
    // Authenticated
    
    // Post action
    
    [Test]
    [AutoData]
    public void Should_create_new_customer_for_post_request_type()
    {
      var command = new Create.Command
      {
        Id = "ABCDE",
        CompanyName = "Company Name",
        ContactName = "Contact Name",
        ContactTitle = "Mr",
        Address = "Address",
        City = "City",
        Region = "Region",
        PostalCode = "ABC",
        Country = "UK",
        Phone = "000",
        Fax = "000",
      };
      
      MyController<CustomersController>
        .Instance()
        .Calling(c => c.Create(command))
        .ShouldHave()
        .ActionAttributes(attributes => attributes.RestrictingForHttpMethod(HttpMethod.Post));
      
    }

    public void Should_fetch_customer_using_the_correct_id()
    {
      var query = new Edit.Query
      {
        Id = "ABCDE"
      };
      
      MyController<CustomersController>
        .Calling(c => c.Edit(query))
        .ShouldReturn();
    }
    
  }
  
}