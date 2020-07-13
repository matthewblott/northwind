namespace Northwind.Infrastructure
{
  using Common;
  using Microsoft.AspNet.Identity;
  using Microsoft.AspNetCore.Authentication.Cookies;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  public static class DependencyInjection
  {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
      IConfiguration configuration, IWebHostEnvironment environment)
    {
      services.AddScoped<IDateTime, MachineDateTime>();
      services.AddScoped<IPasswordHasher, PasswordHasher>();
      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

      return services;

    }
    
  }
  
}