namespace Northwind.WebUI.Features.Account
{
  using System.Security.Claims;
  using System.Threading.Tasks;
  using Common;
  using MediatR;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Authentication.Cookies;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Application.Account.Commands;
  using Northwind.Common;

  public class AccountController : Controller
  {
    private readonly IMediator _mediator;
    private readonly IDateTime _dateTime;

    public AccountController(IMediator mediator, IDateTime dateTime)
    {
      _mediator = mediator;
      _dateTime = dateTime;
    }

    [AllowAnonymous]
    public IActionResult Login() => View(new LoginViewModel());

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(Login.Command command)
    {
      const int timeoutMinutes = 20;
      
      var principal = await _mediator.Send(command);
      
      var props = new AuthenticationProperties
      {
        IsPersistent = true,
        ExpiresUtc = _dateTime.UtcNow.AddMinutes(timeoutMinutes),
      };
      
      await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, (ClaimsPrincipal)principal, props);

      return this.SafeRedirect("Home");
      
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

      return this.SafeRedirect("Home");
      
    }
    
  }
  
}