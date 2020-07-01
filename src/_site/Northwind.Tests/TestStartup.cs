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

      services.ReplaceDbContext();  

      services.ReplaceTransient<IDateTime>(_ => MachineDateTimeMock.Instance);

    }
    
  }
  
}