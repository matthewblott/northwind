namespace Northwind.Tests
{
  using MyTested.AspNetCore.Mvc;
  using NUnit.Framework;
  using WebUI.Features.Home;

  public class HomeControllerTests
  {
    [SetUp]
    public void Setup()
    {
      
    }

    [Test]
    public void IndexShouldReturnView()
    {
      MyController<HomeController>.Instance().Calling(c => c.Index())
        .ShouldReturn()
        .View();
      
    }

  }
  
}