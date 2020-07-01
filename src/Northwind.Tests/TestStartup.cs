namespace Northwind.Tests
{
  using Common;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Mocks;
  using MyTested.AspNetCore.Mvc;
  using WebUI;

  public class TestStartup : Startup
  {
    public TestStartup(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
    {
      
    }

    public void ConfigureTestServices(IServiceCollection services)
    {
      base.ConfigureServices(services);

      // Test framework adds to Configuration Providers with the sources
      // testconfig.json and testsettings.json
      // It may be preferable to configure settings in one of the above files
      // and skip running the ReplaceDbContext method to keep the code cleaner.
      services.ReplaceDbContext();  

      services.ReplaceTransient<IDateTime>(_ => MachineDateTimeMock.Instance);

    }
    
  }
  
}