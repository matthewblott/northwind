namespace Northwind.Infrastructure
{
  using Application.Common.Interfaces;
  using Common;
  using Files;
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
      services.AddScoped<ICsvFileReader, CsvFileReader>();
      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

      return services;

    }
    
  }
  
}