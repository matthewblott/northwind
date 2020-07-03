namespace Northwind.Tests
{
  using System.Linq;
  using Common;
  using Domain;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.EntityFrameworkCore;
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
      ConfigureServices(services);

      var descriptor = services.SingleOrDefault(
        d => d.ServiceType ==
             typeof(DbContextOptions<NorthwindDbContext>));

      if (descriptor != null)
      {
        services.Remove(descriptor);
      }

      var descriptor2 = services.SingleOrDefault(
        d => d.ServiceType ==
             typeof(NorthwindDbContext));
      
      if (descriptor2 != null)
      {
        services.Remove(descriptor2);
      }
      
      services.AddDbContext<NorthwindDbContext>(options =>
        options.UseSqlite(Configuration.GetConnectionString("NorthwindDatabase"))
          .EnableSensitiveDataLogging()
      );

      services.ReplaceTransient<IDateTime>(_ => MachineDateTimeMock.Instance);
    }
  }
}