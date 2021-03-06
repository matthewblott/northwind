namespace Northwind.Tests.Integration
{
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Mvc.Authorization;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.Extensions.DependencyInjection;
  using WebUI;
  using WebUI.Filters;

  public class TestWebApplicationFactory : WebApplicationFactory<Startup>
  {
    protected void ConfigureWebHost2(IWebHostBuilder builder)
    {
      var factory = WithWebHostBuilder(hostBuilder =>
      {
        hostBuilder.ConfigureServices(services =>
        {
          const bool b = false;
          services.AddMvc(options =>
          {
            options.Filters.Add(new AllowAnonymousFilter());
            options.Filters.Add(new FakeUserFilter());
          })
          .AddApplicationPart(typeof(Startup).Assembly);
        });
      });
    }
    
  }
  
}