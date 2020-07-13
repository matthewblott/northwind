namespace RazorPagesProject.Tests
{
  using System.Net;
  using System.Net.Http.Headers;
  using System.Security.Claims;
  using System.Text.Encodings.Web;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using Northwind.WebUI;
  using Xunit;

  public class AuthTests : IClassFixture<CustomWebApplicationFactory<Startup>>
  {
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public AuthTests(CustomWebApplicationFactory<Startup> factory) => _factory = factory;

    [Fact]
    public async Task Get_SecurePageRedirectsAnUnauthenticatedUser()
    {
      // Arrange
      var client = _factory.CreateClient(
        new WebApplicationFactoryClientOptions
        {
          AllowAutoRedirect = false
        });

      // Act
      var response = await client.GetAsync("/customers");

      // Assert
      Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
      Assert.StartsWith("/Account/Login",
        response.Headers.Location.AbsolutePath);
    }

    [Fact]
    public async Task Get_SecurePageIsReturnedForAnAuthenticatedUser()
    {
      // Arrange
      var client = _factory.WithWebHostBuilder(builder =>
        {
          builder.ConfigureTestServices(services =>
          {
            services.AddAuthentication("Test")
              .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                "Test", options => { });
          });
        })
        .CreateClient(new WebApplicationFactoryClientOptions
        {
          AllowAutoRedirect = false,
        });

      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

      //Act
      var response = await client.GetAsync("/customers");

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Get_SecurePageIsReturnedForAnAuthenticatedAdminsUser()
    {
      // Arrange
      var client = _factory.WithWebHostBuilder(builder =>
        {
          builder.ConfigureTestServices(services =>
          {
            services.AddAuthentication("Test")
              .AddScheme<AuthenticationSchemeOptions, TestAuthAdminsHandler>(
                "Test", options => { });
          });
        })
        .CreateClient(new WebApplicationFactoryClientOptions
        {
          AllowAutoRedirect = false,
        });

      client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Test");

      //Act
      var response = await client.GetAsync("/Secure/Admins");

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

  }

  // Handlers
  public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
  {
    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
      ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
      : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      var claims = new[] { new Claim(ClaimTypes.Name, "user1") };
      var identity = new ClaimsIdentity(claims, "Test");
      var principal = new ClaimsPrincipal(identity);
      var ticket = new AuthenticationTicket(principal, "Test");
      var result = AuthenticateResult.Success(ticket);

      return Task.FromResult(result);
    }
    
  }

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