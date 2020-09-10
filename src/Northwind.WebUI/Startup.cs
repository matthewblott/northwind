[assembly: JetBrains.Annotations.AspMvcViewLocationFormat(@"~\Features\{1}\{0}.cshtml")]
[assembly: JetBrains.Annotations.AspMvcViewLocationFormat(@"~\Features\{0}.cshtml")]
[assembly: JetBrains.Annotations.AspMvcViewLocationFormat(@"~\Features\Shared\{0}.cshtml")]
[assembly: JetBrains.Annotations.AspMvcPartialViewLocationFormat(@"~\Features\Shared\{0}.cshtml")]

namespace Northwind.WebUI
{
  using Application;
  using Application.Common.Interfaces;
  using Application.Common.Validators;
  using Common;
  using Features.Products;
  using Filters;
  using FluentValidation.AspNetCore;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Mvc.Authorization;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Infrastructure;
  using Persistence;
  using Services;
  using Validators;

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
      var connectionString = Configuration.GetConnectionString("NorthwindDatabase");
      
      services.AddInfrastructure(Configuration, Environment);
      services.AddPersistence(connectionString);
      services.AddApplication();
      services.AddUrlHelper();
      services.AddCurrentUserService();
      services.AddHttpContextAccessor();
      services.AddControllersWithViews(options => options.Filters.Add(typeof(DbContextTransactionFilter)))
        .AddNewtonsoftJson()
        .AddFeatureFolders()
        .AddFluentValidation(fv =>
        {
          fv.RegisterValidatorsFromAssemblyContaining<INorthwindDbContext>();
          fv.ConfigureClientsideValidation(clientSideValidation =>
          {
            clientSideValidation.Add(typeof(RemoteValidator), (context, rule, validator) => new RemoteClientValidator(rule, validator));
          });
        });

      // Add framework services.
      
      var builder = services.AddMvc(o =>
      {
        var policy = new AuthorizationPolicyBuilder()
          .RequireAuthenticatedUser()
          .Build();
        // o.ModelBinderProviders.Insert(0, new MyModelBinderProvider());
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