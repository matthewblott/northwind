namespace Northwind.Tests.Integration
{
  using System;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using Helpers;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.Extensions.DependencyInjection;
  using NUnit.Framework;
  using Shouldly;
  using WebUI;

  [TestFixture]
  public class CustomerTests
  {
    private WebApplicationFactory<Startup> _factory;
    private HttpClient _client;

    [OneTimeSetUp]
    public void Init()
    {
      Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
      
      var connectionString = Utilities.GetTestDatabaseConnectionString();
      
      Environment.SetEnvironmentVariable("ConnectionStrings__NorthwindDatabase", connectionString);

      _factory = new WebApplicationFactory<Startup>();
      
      var builder = _factory.WithWebHostBuilder(hostBuilder =>
      {
        hostBuilder.ConfigureTestServices(services =>
        {
          services.AddAuthentication("Test")
            .AddScheme<AuthenticationSchemeOptions, TestAuthAdminsHandler>(
              "Test", options => { });
          
        });

      });
      
      _client = builder.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
      
    }
    
    [Test]
    public async Task Should_navigate_to_customer_index_page()
    {
      // Act
      const string url = "/customers";
      
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

      response.Dispose();

    }
    
    [Test]
    public async Task Should_navigate_to_customer_page()
    {
      // Arrange
      const string customerId = "alfki";
      const string companyName = "Alfreds Futterkiste";
      var url = $"/customers/edit/{customerId}";

      // Act
      var response = await _client.GetAsync(url);

      var content = await HtmlHelpers.GetDocumentAsync(response);
      
      var idElement = content.QuerySelector("#CompanyName");
      
      idElement.Attributes["value"].Value.ShouldBe(companyName);
      
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
      
      response.Dispose();
      
    }

    [OneTimeTearDown]
    public void TearDown()
    {
      _client.Dispose();
      _factory.Dispose();
    }
    
  }
  
}