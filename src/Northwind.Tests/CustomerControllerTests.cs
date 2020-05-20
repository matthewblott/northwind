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
    [Test]
    [AutoData]
    public void PostCreateShouldBeAllowedOnlyForPostRequest()
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
        .Instance().Calling(c => c.Create(command))
        .ShouldHave()
        .ActionAttributes(attributes => attributes.RestrictingForHttpMethod(HttpMethod.Post));
      
    }
    
  }
  
}