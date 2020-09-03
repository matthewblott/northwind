namespace Northwind.Tests.Integration
{
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using Helpers;
  using NUnit.Framework;
  using Shouldly;
  using WebUI;

  [TestFixture]
  public class HomeTests
  {
    private TestWebApplicationFactory _factory;
    private HttpClient _client;
    
    [OneTimeSetUp]
    public void Init()
    {
      // Arrange
      _factory = new TestWebApplicationFactory();
      _client = _factory.CreateClient();
    }
    
    [Test]
    public async Task Should_navigate_home_successfully()
    {
      const string url = "/";
      
      // Act
      var response = await _client.GetAsync(url);

      // Assert
      response
        .EnsureSuccessStatusCode()
        .StatusCode
        .ShouldBe(HttpStatusCode.OK);
      
      response
        .Content
        .Headers
        .ContentType
        .ToString()
        .ShouldBe("text/html; charset=utf-8");
      
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
      _client.Dispose();
      _factory.Dispose();
    }
    
  }
  
}