[assembly: JetBrains.Annotations.AspMvcViewLocationFormat(@"~\Features\{1}\{0}.cshtml")]
[assembly: JetBrains.Annotations.AspMvcViewLocationFormat(@"~\Features\{0}.cshtml")]
[assembly: JetBrains.Annotations.AspMvcViewLocationFormat(@"~\Features\Shared\{0}.cshtml")]
[assembly: JetBrains.Annotations.AspMvcPartialViewLocationFormat(@"~\Features\Shared\{0}.cshtml")]

namespace Northwind.WebUI
{
  using Application;
  using Application.Common.Interfaces;
  using Common;
  using Filters;
  using FluentValidation.AspNetCore;
  using Microsoft.AspNet.Identity;
  using Microsoft.AspNetCore.Authentication.Cookies;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Mvc.Authorization;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Infrastructure;
  using Northwind.Common;
  using Persistence;
  using Services;

  public class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
      Configuration = configuration;
      Environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddInfrastructure(Configuration, Environment);
      services.AddPersistence(Configuration);
      services.AddApplication();
      services.AddScoped<ICurrentUserService, CurrentUserService>();
      services.AddHttpContextAccessor();
      services.AddControllersWithViews(options => options.Filters.Add(typeof(DbContextTransactionFilter)))
        .AddNewtonsoftJson()
        .AddFeatureFolders()
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<INorthwindDbContext>());

      services.AddScoped<ICurrentUserService, CurrentUserService>();

      // services.AddDbContext(Configuration);
      
      var builder = services.AddMvc(o =>
      {
        var policy = new AuthorizationPolicyBuilder()
          .RequireAuthenticatedUser()
          .Build();
        o.Filters.Add(new AuthorizeFilter(policy));
      });

      var settings = Configuration.GetSection(nameof(RazorSettings)).Get<RazorSettings>();
      var isAllowed = (settings ?? new RazorSettings()).AllowRuntimeCompilation;
      
      if (isAllowed)
      {
        builder.AddRazorRuntimeCompilation();
      }
      
      services.AddRouting(option => option.LowercaseUrls = true);

    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseStaticFiles();
      app.UseTurboLinks();
      app.UseRouting();
      app.UseAuthentication();
      app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
    }
    
  }
  
}