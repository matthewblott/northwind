namespace Northwind.WebUI.Common
{
  using Microsoft.AspNetCore.Builder;

  public static class TurbolinksBuilderExtension
  {
    public static IApplicationBuilder UseTurboLinks(this IApplicationBuilder app) => app.UseMiddleware<TurboLinks>();
  }
}