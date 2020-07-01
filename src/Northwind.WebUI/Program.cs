namespace Northwind.WebUI
{
  using System;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Hosting;
  using Microsoft.Extensions.Logging;
  using NLog;
  using NLog.Web;

  public static class Program
  {
    public static int Main(string[] args)
    {
      var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
      
      try
      {
        logger.Debug("init main");
        
        var host = CreateHost(args);
        
        host.Run();
        
        return 0;
      }
      catch (Exception ex)
      {
        logger.Fatal(ex, "Stopped program because of exception");
        
        return 1;
      }
      finally
      {
        LogManager.Shutdown();
      }

    }

    private static IHost CreateHost(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
        .ConfigureLogging(logging =>
        {
          logging.ClearProviders();
        })
        .UseNLog()
        .Build();

  }
  
}