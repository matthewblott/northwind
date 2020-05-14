namespace Northwind.WebUI
{
  using Microsoft.AspNetCore;
  using Microsoft.AspNetCore.Hosting;
  using Setup;

  public class Program
  {
    public static void Main(string[] args) 
      => WebHost
        .CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .Build()
        .Run();

  }
}