namespace RazorPagesProject.Tests
{
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Mvc.Testing;

  public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
  {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      builder.ConfigureServices(options =>
      {
        
      });
    }
    
  }
  
}