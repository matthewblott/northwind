namespace Northwind.Tests
{
  using System.Linq;
  using System.Threading.Tasks;
  using Application.Customers.Commands;
  using Application.Customers.Queries;
  using MediatR;
  using Microsoft.AspNetCore.Mvc;
  using Mocks;
  using NUnit.Framework;
  using Shouldly;
  using WebUI.Features.Customers;

  [TestFixture]
  public class CustomerTests
  {
    [Test]
    public async Task Should_receive_correct_number_of_items_from_request()
    {
      // Arrange
      var controller = new CustomersController(TestFixture.GetInstance<IMediator>());

      // Act
      var result = await controller.Index() as ViewResult;

      // Assert
      var model = result?.ViewData.Model as Index.Model;

      model?.Items.FirstOrDefault().ShouldBeOfType<Index.Model.Item>();
      model?.Items.Count.ShouldBe(52); // 93 in test db

    }

    [Test]
    public async Task Should_receive_correct_customer_details_from_request()
    {
      // Arrange
      var controller = new CustomersController(TestFixture.GetInstance<IMediator>());

      const string customerId = "ANATR";

      var expectedModel = DataMocks.GetCustomerList(0)
        .Single(c => c.CustomerId == customerId);
      
      // Act
      var result = await controller.Edit(new Edit.Query { Id = customerId }) as ViewResult;
      
      // Assert
      var model = result?.ViewData.Model as Edit.Command;

      model?.Id.ShouldBe(expectedModel.CustomerId);
      model?.CompanyName.ShouldBe(expectedModel.CompanyName);
      model?.ContactName.ShouldBe(expectedModel.ContactName);
      model?.ContactTitle.ShouldBe(expectedModel.ContactTitle);
      model?.Address.ShouldBe(expectedModel.Address);
      model?.City.ShouldBe(expectedModel.City);
      model?.Region.ShouldBe(expectedModel.Region);
      model?.PostalCode.ShouldBe(expectedModel.PostalCode);
      model?.Country.ShouldBe(expectedModel.Country);
      model?.Phone.ShouldBe(expectedModel.Phone);
      model?.Fax.ShouldBe(expectedModel.Fax);
      
    }
    
  }
  
}