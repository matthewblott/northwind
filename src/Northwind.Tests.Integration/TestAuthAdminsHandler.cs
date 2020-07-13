namespace Northwind.Tests.Integration
{
  using System;
  using System.Collections.Generic;
  using System.Security.Claims;
  using System.Text.Encodings.Web;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  public class TestAuthAdminsHandler : AuthenticationHandler<AuthenticationSchemeOptions>
  {
    public TestAuthAdminsHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
      ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
      : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      var claims = new[]
      {
        new Claim(ClaimTypes.Name, "user1"),
        new Claim(ClaimTypes.Role, "Admins"),
      };

      var identity = new ClaimsIdentity(claims, "Test");
      var principal = new ClaimsPrincipal(identity);
      var ticket = new AuthenticationTicket(principal, "Test");
      var result = AuthenticateResult.Success(ticket);

      return Task.FromResult(result);
      
    }
    
  }
  
}