namespace Northwind.Application
{
  using System.Reflection;
  using AutoMapper;
  using MediatR;
  using Microsoft.Extensions.DependencyInjection;
  using WebUI.Behaviours;

  public static class DependencyInjection
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddMediatR(Assembly.GetExecutingAssembly());
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

      return services;
      
    }
    
  }
  
}