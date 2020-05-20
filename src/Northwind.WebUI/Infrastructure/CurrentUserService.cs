using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Northwind.Application.Common.Interfaces;

namespace Northwind.WebUI.Infrastructure
{
  public class CurrentUserService : ICurrentUserService
  {
    // Remember to add the configuration for IHttpContextAccessor in Startup
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
      UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
      IsAuthenticated = UserId != null;
    }

    public string UserId { get; }

    public bool IsAuthenticated { get; }
  }
}