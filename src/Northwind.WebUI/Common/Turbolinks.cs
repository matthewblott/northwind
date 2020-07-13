namespace Northwind.WebUI.Common
{
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Http;

  public class TurboLinks
  {
    private readonly RequestDelegate _next;

    public TurboLinks(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
      // See https://github.com/turbolinks/turbolinks#following-redirects
      if (context.Response.StatusCode == 302 || context.Response.StatusCode == 301)
      {
        context.Response.OnStarting(state =>
        {
          var httpContext = (HttpContext)state;

          if (!httpContext.Response.Headers.ContainsKey("Turbolinks-Location"))
          {
            httpContext.Response.Headers.Add("Turbolinks-Location", new[]
            {
              httpContext.Response.Headers["Location"].ToString()
            });
          }
          
          return Task.FromResult(0);
        }, context);

      }
      
      await _next(context);
      
    }
    
  }
  
}