namespace Northwind.Persistence
{
  using Application.Common.Interfaces;
  using Domain;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  public static class DependencyInjection
  {
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
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

      services.AddScoped<INorthwindDbContext>(provider => provider.GetRequiredService<NorthwindDbContext>());
      services.AddScoped<IDbContextTransaction>(provider => provider.GetRequiredService<NorthwindDbContext>());
      
      return services;
      
    }
    
  }
  
}