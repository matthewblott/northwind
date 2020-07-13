namespace Northwind.Tests.Mocks
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Security.Claims;
  using Application.Common.Interfaces;
  using Domain;
  using FakeItEasy;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Logging;
  using Persistence;
  using WebUI;

  public static class TestFixture
  {
    private static readonly ServiceProvider Provider;
    private static readonly IHttpContextAccessor HttpContextAccessor;

    public static ClaimsPrincipal ClaimPrincipal { get; set; }

    static TestFixture()
    {
      var dict = new Dictionary<string, string>
      {
        {"GoogleClientId", ""},
        {"MailJetApiKey", ""},
        {"test", "true"},
      };

      var config = new ConfigurationBuilder()
        .AddInMemoryCollection(dict)
        .Build();

      IWebHostEnvironment environment = null;
      
      var startup = new Startup(config, environment);
      var services = new ServiceCollection();
      
      startup.ConfigureServices(services);

      // Swap out services here
      
      var descriptor = services.SingleOrDefault(
        d => d.ServiceType ==
             typeof(DbContextOptions<NorthwindDbContext>));

      if (descriptor != null)
      {
        services.Remove(descriptor);
      }
      
      // services.AddDbContext<NorthwindDbContext>(options =>
      //   options.UseSqlite(Utilities.GetTestDatabaseConnectionString())
      //     .EnableSensitiveDataLogging());

      var descriptor2 = services
        .SingleOrDefault(d => d.ServiceType == typeof(INorthwindDbContext));
      
      if (descriptor2 != null)
      {
        services.Remove(descriptor2);
      }

      var descriptor3 = services
        .SingleOrDefault(d => d.ServiceType == typeof(IDbContextTransaction));
      
      if (descriptor3 != null)
      {
        services.Remove(descriptor3);
      }

      services.AddScoped<TestDbContext>();
      services.AddScoped<INorthwindDbContext>(provider => provider.GetRequiredService<TestDbContext>());
      services.AddScoped<IDbContextTransaction>(provider => provider.GetRequiredService<TestDbContext>());

      // Continue 
      HttpContextAccessor = ContextAccessorMock.Instance;

      services.AddSingleton(HttpContextAccessor);
      services.AddScoped(typeof(ILoggerFactory), typeof(LoggerFactory));
      services.AddScoped(typeof(ILogger<>), typeof(Logger<>));

      Provider = services.BuildServiceProvider();
    }

    private static void SetControllerContext(Controller controller)
    {
      controller.ControllerContext = new ControllerContext
      {
        HttpContext = HttpContextAccessor.HttpContext
      };
    }

    private static void SetControllerContext(ControllerBase controller)
    {
      controller.ControllerContext = new ControllerContext
      {
        HttpContext = HttpContextAccessor.HttpContext
      };
    }

    public static T GetInstance<T>()
    {
      var result = Provider.GetRequiredService<T>();
      if (result is ControllerBase controllerBase)
      {
        SetControllerContext(controllerBase);
      }

      if (result is Controller controller)
      {
        SetControllerContext(controller);
      }

      return result;
    }
  }
}