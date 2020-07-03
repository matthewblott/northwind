namespace Northwind.Tests
{
  using Application.Customers.Commands;
  using Application.Customers.Queries;
  using AutoFixture;
  using AutoFixture.NUnit3;
  using MyTested.AspNetCore.Mvc;
  using NUnit.Framework;
  using Shouldly;
  using WebUI.Features.Customers;
  using HttpMethod = System.Net.Http.HttpMethod;

  public class CustomerControllerTests
  {
    // Authenticated

    // Post action

    [Test]
    public void Should_show_customers()
    {
      MyController<CustomersController>
        .Calling(x => x.Index())
        .ShouldReturn()
        .View(v => v.WithModelOfType<Index.Model>()
          .Passing(x => x.Items.Count.ShouldBe(0)));
    }

    [Test]
    [AutoData]
    public void Should_create_new_customer_for_post_request_type()
    {
      var fixture = new Fixture();

      var command = new Create.Command
      {
        Id = fixture.Create<string>().Substring(0, 5),
        CompanyName = fixture.Create<string>().Substring(0, 20),
        ContactName = fixture.Create<string>().Substring(0, 20),
        ContactTitle = fixture.Create<string>().Substring(0, 4),
        Address = fixture.Create<string>().Substring(0, 10),
        City = fixture.Create<string>().Substring(0, 10),
        Region = fixture.Create<string>().Substring(0, 10),
        PostalCode = fixture.Create<string>().Substring(0, 10),
        Country = fixture.Create<string>().Substring(0, 10),
        Phone = fixture.Create<byte>().ToString(),
        Fax = fixture.Create<byte>().ToString(),
      };

      MyController<CustomersController>
        .Calling(c => c.Create(command))
        .ShouldHave()
        .ActionAttributes(attributes => attributes.RestrictingForHttpMethod(HttpMethod.Post));
    }

    [Test] // AutoData
    // ReSharper disable once NUnit.MethodWithParametersAndTestAttribute
    public void Should_fetch_customer_using_the_correct_id()
    {
      const string customerId = "ANTON";
      
      var query = new Edit.Query
      {
        Id = customerId
      };

      MyController<CustomersController>
        .Instance()
        .WithData()
        .Calling(c => c.Edit(query))
        .ShouldReturn()
        .View(view => view
          .WithModelOfType<Edit.Command>()
          .Passing(x => x.Id.ShouldBe(query.Id)));
    }
  }
}