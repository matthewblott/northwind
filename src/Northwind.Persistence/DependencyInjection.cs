namespace Northwind.Persistence
{
  using Application.Common.Interfaces;
  using Domain;
  using Domain.Common;
  using Mappings;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Storage;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Thinktecture;
  using IDbContextTransaction = Application.Common.Interfaces.IDbContextTransaction;

  public static class DependencyInjection
  {
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
      services.AddDbContext<NorthwindDbContext>(options =>
      {
        var enabled = false;
#if DEBUG
        enabled = true;
#endif
        options.AddRelationalTypeMappingSourcePlugin<IdTypeMappingPlugin>();
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        options.UseSqlite(connectionString)
          .EnableSensitiveDataLogging(enabled);
      });

      services.AddScoped<INorthwindDbContext>(provider => provider.GetRequiredService<NorthwindDbContext>());
      services.AddScoped<IDbContextTransaction>(provider => provider.GetRequiredService<NorthwindDbContext>());
      
      return services;
      
    }
    
  }

  public interface IMyInterface : IRelationalTypeMappingSourcePlugin
  {
    
  }
  
}