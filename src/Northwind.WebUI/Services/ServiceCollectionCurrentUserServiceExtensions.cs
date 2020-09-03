namespace Northwind.WebUI.Services
{
  using Microsoft.Extensions.DependencyInjection;
  using Northwind.Common;

  public static class ServiceCollectionCurrentUserServiceExtensions
  {
    public static void AddCurrentUserService(this IServiceCollection services)
    {
      services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
    
  }
  
}