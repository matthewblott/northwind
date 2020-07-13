namespace Northwind.Tests.Integration
{
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using Helpers;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using NUnit.Framework;
  using Shouldly;
  using WebUI;

  [TestFixture]
  public class CustomerTests
  {
    private TestWebApplicationFactory _factory;
    private HttpClient _client;

    [OneTimeSetUp]
    public void Init()
    {
      // Arrange
      _factory = new TestWebApplicationFactory();

      _client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
      
    }
    
    [Test]
    public async Task Should_navigate_to_customer_index_page()
    {
      // Arrange
      var hostBuilder = new HostBuilder()
        .ConfigureWebHost(webHost =>
        {
          // Add TestServer
          webHost.UseTestServer();
          webHost.UseStartup<Startup>();

          // configure the services after the startup has been called.
          webHost.ConfigureTestServices(services =>
          {
            services.AddAuthentication("Test")
              .AddScheme<AuthenticationSchemeOptions, TestAuthAdminsHandler>(
                "Test", options => { });
          });

        });
      
      // Build and start the IHost
      var host = await hostBuilder.StartAsync();

      // Create an HttpClient to send requests to the TestServer
      var client = host.GetTestClient();
      
      // Act
      var response = await client.GetAsync("/customers");

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

    [Test]
    public async Task Should_navigate_to_customer_page()
    {
      const string customerId = "alfki";
      
      // ANTON
      
      var url = $"/customers/edit/{customerId}";

      // Act
      var response = await _client.GetAsync(url);

      var content = await HtmlHelpers.GetDocumentAsync(response);
      
      var idElement = content.QuerySelector("#Username");
      
      idElement.Attributes["value"].Value.ShouldBe(string.Empty);
      
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