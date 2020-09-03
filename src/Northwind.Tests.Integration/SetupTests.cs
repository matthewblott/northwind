namespace Northwind.Tests.Integration
{
  using System.Threading.Tasks;
  using Helpers;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.Extensions.Hosting;
  using NUnit.Framework;

  [TestFixture]
  public class SetupTests
  {
    [Test]
    public async Task Should_run_simple_hello_world_successfully()
    {
      var hostBuilder = new HostBuilder()
        .ConfigureWebHost(webHost =>
        {
          webHost.UseTestServer();
          webHost.Configure(app =>
            app.Run(async ctx => await ctx.Response.WriteAsync("Hello World!"))
          );
        });

      var host = await hostBuilder.StartAsync();
      var client = host.GetTestClient();
      var response = await client.GetAsync("/");

      response.EnsureSuccessStatusCode();

      var responseString = await response.Content.ReadAsStringAsync();

      Assert.IsTrue("Hello World!" == responseString);
    }

    [Test]
    public async Task Should_configure_test_database()
    {
      await DataHelper.Configure();
    }

  }
  
}