namespace Northwind.WebUI.Setup
{
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Northwind.Application.Common.Interfaces;
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
      services.AddInfrastructure();
      services.AddPersistence(Configuration);
      services.AddApplication();
      services.AddScoped<ICurrentUserService, CurrentUserService>(); // Move to an extension method
      services.AddMvcServices(Configuration.GetRazorSettings());
      services.AddHttpContextAccessor();
      services.AddRoutingOptions();
      services.AddProjectControllers();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseEndpoints();
    }
  }
}