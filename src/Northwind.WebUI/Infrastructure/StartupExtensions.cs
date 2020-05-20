namespace Northwind.WebUI.Infrastructure
{
  using System;
  using Application;
  using AutoMapper;
  using Behaviours;
  using MediatR;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Northwind.Application.Common.Interfaces;
  using Common;
  using Domain;
  using Filters;
  using FluentValidation.AspNetCore;

  public static class StartupExtensions
  {
    public static void AddApplication(this IServiceCollection services)
    {
      Caller.Call();

      foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
      {
        if (!assembly.GetName().Name?.Contains("Application") ?? false)
          continue;

        services.AddAutoMapper(assembly);
        services.AddMediatR(assembly);
      }

      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    public static void AddServices(this IServiceCollection services)
    {
      services.AddScoped<IDateTime, MachineDateTime>();
      services.AddScoped<ICurrentUserService, CurrentUserService>();
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<NorthwindDbContext>(options =>
      {
        var enabled = false;
#if DEBUG
        enabled = true;
#endif
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        options.UseSqlite(configuration.GetConnectionString("NorthwindDatabase"))
          .EnableSensitiveDataLogging(enabled);
      });

      services.AddScoped<INorthwindDbContext>(provider => provider.GetService<NorthwindDbContext>());
    }

    public static void AddProjectControllers(this IServiceCollection services)
    {
      services.AddControllersWithViews(options => { options.Filters.Add(typeof(DbContextTransactionFilter)); })
        .AddNewtonsoftJson()
        .AddFeatureFolders()
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Caller>());
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
      => app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
  }
}