namespace Northwind.Tests
{
  using Microsoft.AspNetCore.Mvc;
  using NUnit.Framework;
  using Shouldly;
  using WebUI.Features.Home;

  [TestFixture]
  public class HomeTests
  {
    [Test]
    public void Should_return_index_view()
    {
      // Arrange
      var controller = new HomeController();
      
      // Act
      var result = controller.Index();

      // Assert
      result.ShouldBeOfType<ViewResult>();
    }
    
  }
  
}