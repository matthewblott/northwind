namespace Northwind.WebUI.Infrastructure
{
  using Microsoft.AspNetCore.Builder;

  public static class TurbolinksBuilderExtension
  {
    public static IApplicationBuilder UseTurboLinks(this IApplicationBuilder app) => app.UseMiddleware<TurboLinks>();
  }
}