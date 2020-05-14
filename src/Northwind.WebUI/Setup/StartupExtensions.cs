namespace Northwind.WebUI.Setup
{
  using System;
  using AutoMapper;
  using MediatR;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Application;
  using Northwind.Application.Common.Behaviours;
  using Northwind.Application.Common.Interfaces;
  using Common;
  using Domain;
  using Filters;
  using FluentValidation.AspNetCore;

  public static class StartupExtensions
  {
    public static void AddApplication(this IServiceCollection services)
    {
      foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
      {
        if (!assembly.GetName().Name?.Contains("Application") ?? false)
          continue;
        
        services.AddAutoMapper(assembly);
        services.AddMediatR(assembly);
        
      }
      
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
      
    }
    
    public static void AddInfrastructure(this IServiceCollection services)
    {
      services.AddTransient<INotificationService, NotificationService>();
      services.AddTransient<IDateTime, MachineDateTime>();
    }
    
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<NorthwindDbContext>(options =>
      {
        options.UseSqlite(configuration.GetConnectionString("NorthwindDatabase"));
      });

      services.AddScoped<INorthwindDbContext>(provider => provider.GetService<NorthwindDbContext>());
    }

    public static void AddProjectControllers(this IServiceCollection services)
    {
      services.AddControllersWithViews(options =>
        {
          options.Filters.Add(typeof(DbContextTransactionFilter));
        })
        .AddNewtonsoftJson()
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<INorthwindDbContext>());
    }
    
    public static void AddRoutingOptions(this IServiceCollection services)
      => services.AddRouting(option => option.LowercaseUrls = true);

    public static void AddMvcServices(this IServiceCollection services, RazorSettings settings)
    {
      var builder = services.AddMvc();
      
      var isAllowed = (settings ?? new RazorSettings()).AllowRuntimeCompilation;
      
      if (isAllowed)
      {
        builder.AddRazorRuntimeCompilation();
      }
    }

    public static RazorSettings GetRazorSettings(this IConfiguration configuration)
      => configuration.GetSection(nameof(RazorSettings)).Get<RazorSettings>();

    public static void UseEndpoints(this IApplicationBuilder app)
    {
      app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
    }
  }
}