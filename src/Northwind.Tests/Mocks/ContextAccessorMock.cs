namespace Northwind.Tests.Mocks
{
  using System.Security.Claims;
  using FakeItEasy;
  using Microsoft.AspNetCore.Http;

  public static class ContextAccessorMock
  {
    public static IHttpContextAccessor Instance
    {
      get
      {
        var accessor = A.Fake<IHttpContextAccessor>();
      
        var claims = new[]
        {
          new Claim(ClaimTypes.Name, "user1"),
          new Claim(ClaimTypes.Role, "Admins"),
        };

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
      
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "1234"));
        
        A.CallTo(() => accessor.HttpContext)
          .Returns(new DefaultHttpContext { User = principal });
        
        return accessor;
      }
    }
  }
}