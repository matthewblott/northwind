namespace Northwind.Tests.Integration
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Net.Http;
  using System.Threading.Tasks;
  using Helpers;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.Extensions.DependencyInjection;
  using NUnit.Framework;
  using WebUI;

  public class AccountTests
  {
    [Test]
    public async Task Should_login_successfully()
    {
      // Arrange
      Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
      
      var connectionString = Utilities.GetTestDatabaseConnectionString();
      
      Environment.SetEnvironmentVariable("ConnectionStrings__NorthwindDatabase", connectionString);
      
      var factory = new WebApplicationFactory<Startup>();

      var builder = factory.WithWebHostBuilder(hostBuilder =>
      {
        // hostBuilder.ConfigureTestServices(services =>
        // {
        //   services.AddAuthentication("Test")
        //     .AddScheme<AuthenticationSchemeOptions, TestAuthAdminsHandler>(
        //       "Test", options => { });
        // });

      });

      var client = builder.CreateClient(new WebApplicationFactoryClientOptions {AllowAutoRedirect = false});

      // Act
      const string loginUrl = "/account/login";

      //Arrange            
      var formContent = new FormUrlEncodedContent(new[]
      {
        new KeyValuePair<string, string>("Username", "admin"), 
        new KeyValuePair<string, string>("Password", "letmein") 
      });
 
      var response = await client.PostAsync(loginUrl, formContent);

      response.EnsureSuccessStatusCode();
      
      client.Dispose();
      factory.Dispose();

    }
    
  }
    
}