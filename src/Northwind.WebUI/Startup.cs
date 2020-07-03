[assembly: JetBrains.Annotations.AspMvcViewLocationFormat(@"~\Features\{1}\{0}.cshtml")]
[assembly: JetBrains.Annotations.AspMvcViewLocationFormat(@"~\Features\{0}.cshtml")]
[assembly: JetBrains.Annotations.AspMvcViewLocationFormat(@"~\Features\Shared\{0}.cshtml")]
[assembly: JetBrains.Annotations.AspMvcPartialViewLocationFormat(@"~\Features\Shared\{0}.cshtml")]

namespace Northwind.WebUI
{
  using Infrastructure;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Microsoft.Extensions.Logging;
  using NLog.Web;

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
      services.AddServices();
      services.AddMyAuthentication();
      services.AddDbContext(Configuration);
      services.AddApplication();
      services.AddMyMvc(Configuration.GetRazorSettings());
      services.AddHttpContextAccessor();
      services.AddMyRouting();
      services.AddMyControllers();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseStaticFiles();
      app.UseTurboLinks();
      app.UseRouting();
      app.UseAuthentication();
      app.UseEndpoints();
    }
    
  }
  
}