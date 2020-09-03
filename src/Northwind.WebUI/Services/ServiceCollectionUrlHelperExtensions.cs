namespace Northwind.WebUI.Services
{
  using Microsoft.AspNetCore.Mvc.Infrastructure;
  using Microsoft.AspNetCore.Mvc.Routing;
  using Microsoft.Extensions.DependencyInjection;

  public static class ServiceCollectionUrlHelperExtensions
  {
    public static void AddUrlHelper(this IServiceCollection services)
    {
      services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
      services.AddScoped(x => {
        var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
        var factory = x.GetRequiredService<IUrlHelperFactory>();
        return factory.GetUrlHelper(actionContext);
      });
      
    }

  }
}