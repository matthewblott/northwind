namespace Northwind.Tests.Integration
{
  using System;
  using System.Threading.Tasks;
  using Helpers;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.Extensions.DependencyInjection;
  using NUnit.Framework;
  using Shouldly;
  using WebUI;

  public class InfoTests
  {
    [Test]
    public async Task Should_return_os_name()
    {
      // Arrange
      Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
      
      var connectionString = Utilities.GetTestDatabaseConnectionString();
      
      Environment.SetEnvironmentVariable("ConnectionStrings__NorthwindDatabase", connectionString);
      
      var factory = new WebApplicationFactory<Startup>();

      var builder = factory.WithWebHostBuilder(hostBuilder =>
      {
        hostBuilder.ConfigureTestServices(services =>
        {
          services.AddAuthentication("Test")
            .AddScheme<AuthenticationSchemeOptions, TestAuthAdminsHandler>(
              "Test", options => { });
          
        });

      });

      var client = builder.CreateClient(new WebApplicationFactoryClientOptions {AllowAutoRedirect = false});
      
      const string url = "/info/os";
      
      var response = await client.GetAsync(url);
      
      var os = await response.Content.ReadAsStringAsync();

      os.ToLower().ShouldStartWith("unix");
      
      response.Dispose();
      client.Dispose();
      factory.Dispose();
      
    }
    
  }
  
}