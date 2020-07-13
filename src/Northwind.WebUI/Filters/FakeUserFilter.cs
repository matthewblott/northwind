namespace Northwind.WebUI.Filters
{
  using System.Collections.Generic;
  using System.Security.Claims;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Authentication.Cookies;
  using Microsoft.AspNetCore.Mvc.Filters;

  public class FakeUserFilter : IAsyncActionFilter
  {
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      const string admins = "Admins";
      const string superUsers = "SuperUsers";
      
      context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
      {
        new Claim(ClaimTypes.NameIdentifier, "123"), 
        new Claim(ClaimTypes.Name, "admin"),
        new Claim(ClaimTypes.Role, admins),
        new Claim(ClaimTypes.Role, superUsers)
      }, CookieAuthenticationDefaults.AuthenticationScheme));

      await next();

    }
    
  }
  
}