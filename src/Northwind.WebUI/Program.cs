namespace Northwind.WebUI
{
  using System;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Hosting;
  using Microsoft.Extensions.Logging;
  using NLog;
  using NLog.Web;
  
  public class Program
  {
    public static int Main(string[] args)
    {
      var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
      
      try
      {
        logger.Debug("init main");
        
        var host = CreateHostBuilder(args)
          .Build();;
        
        host.Run();
        
        return 0;
      }
      catch (Exception ex)
      {
        logger.Fatal(ex, ex.Message);
        
        return 1;
      }
      finally
      {
        LogManager.Shutdown();
      }

    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
        .ConfigureLogging(logging => logging.ClearProviders())
        .UseNLog();

  }
  
}