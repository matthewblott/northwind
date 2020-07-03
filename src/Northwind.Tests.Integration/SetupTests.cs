namespace Northwind.Tests.Integration
{
  using System.Linq;
  using System.Threading.Tasks;
  using AngleSharp.Html.Dom;
  using Domain;
  using Helpers;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.DependencyInjection.Extensions;
  using Microsoft.Extensions.Hosting;
  using NUnit.Framework;
  using Shouldly;
  using WebUI;

  [TestFixture]
  public class SetupTests
  {
    [Test]
    public async Task Should_run_simple_hello_world_successfully()
    {
      var hostBuilder = new HostBuilder()
        .ConfigureWebHost(webHost =>
        {
          webHost.UseTestServer();
          webHost.Configure(app =>
            app.Run(async ctx => await ctx.Response.WriteAsync("Hello World!"))
          );
        });

      var host = await hostBuilder.StartAsync();
      var client = host.GetTestClient();

      //var server = new TestServer(hostBuilder);
      //var client = server.CreateClient();
      var response = await client.GetAsync("/");

      response.EnsureSuccessStatusCode();

      var responseString = await response.Content.ReadAsStringAsync();

      Assert.IsTrue("Hello World!" == responseString);
    }

    [Test]
    public async Task Should_navigate_to_customers_successfully()
    {
      var factory = new WebApplicationFactory<Program>();

      var client = factory.CreateClient();

      var response = await client.GetAsync("/Customers");

      var responseString = await response.Content.ReadAsStringAsync();

      Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [Test]
    public async Task Should_display_an_existing_customer_successfully()
    {
      var factory = new WebApplicationFactory<Program>();

      var client = factory.WithWebHostBuilder(builder =>
        {
          builder.ConfigureTestServices(services =>
          {
            services.RemoveAll(typeof(IHostedService));

            var descriptor = services.SingleOrDefault(
              d => d.ServiceType ==
                   typeof(DbContextOptions<NorthwindDbContext>));

            if (descriptor != null)
            {
              services.Remove(descriptor);
            }

            services.AddDbContext<NorthwindDbContext>(options =>
              options.UseSqlite(Utilities.GetTestDatabaseConnectionString())
                .EnableSensitiveDataLogging()
            );

            services.AddAuthentication("Test")
              .AddScheme<AuthenticationSchemeOptions, TestAuthAdminsHandler>(
                "Test", options => { });
          });
        })
        .CreateClient(new WebApplicationFactoryClientOptions
        {
          AllowAutoRedirect = false,
        });

      const string customerId = "ANTON";

      var response = await client.GetAsync($"/Customers/Edit/{customerId}");
      
      response.IsSuccessStatusCode.ShouldBeTrue();
      
      var content = await HtmlHelpers.GetDocumentAsync(response);

      var element = content.QuerySelector("#CompanyName") as IHtmlInputElement;

      var value = element?.Value;
      
      value.ShouldStartWith("Antonio Moreno");
      
    }
    
  }
  
}