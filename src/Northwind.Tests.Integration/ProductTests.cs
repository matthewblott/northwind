namespace Northwind.Tests.Integration
{
  using System;
  using System.IO;
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
  
  public class ProductTests
  {
    [Test]
    public async Task Should_upload_product_file_successfully()
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
      
      const string url = "/products/import";
      
      // Act
      HttpResponseMessage response;

      var path = Utilities.GetProductFilePath();

      await using (var file = File.OpenRead(path))
      using (var content = new StreamContent(file))
      using (var formData = new MultipartFormDataContent())
      {
        formData.Add(content, "file", "products.csv");
        response = await client.PostAsync(url, formData);
      }
      
      // Assert
      response.EnsureSuccessStatusCode();
      response.ShouldNotBeNull();
      response.Content.Headers.ContentType.ToString().ShouldBe("text/html; charset=utf-8");

      // Check they've imported ...
      
      
      // Clean up      
      response.Dispose();
      client.Dispose();
      factory.Dispose();
      
    }
    
  }
  
}