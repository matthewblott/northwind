namespace Northwind.Database
{
  using System;
  using System.IO;
  using System.Reflection;
  using DbUp;
  using Microsoft.Data.Sqlite;
  using Microsoft.Extensions.Configuration;

  internal static class Program
  {
    private static void Main(string[] args)
    {
      var assembly = Assembly.GetExecutingAssembly();
      var assemblyName = assembly.GetName().Name?.ToLower();
      var info = Directory.GetParent(assembly.Location);

      while (info?.Name.ToLower() != assemblyName)
      {
        info = info?.Parent;
      }

      var builder = new ConfigurationBuilder()
        .SetBasePath(info?.FullName)
        .AddJsonFile("appsettings.json");

      var configuration = builder.Build();
      var setting = configuration.GetConnectionString("NorthwindDatabase");
      var path = Path.Combine(info?.FullName ?? string.Empty, "db") + Path.DirectorySeparatorChar;
      var connStr = string.Format(setting, path);
      var conn = new SqliteConnection(connStr);

      conn.Open();

      var runner =
        DeployChanges.To
          .SQLiteDatabase(conn.ConnectionString)
          .WithScriptsEmbeddedInAssembly(assembly)
          .LogToConsole()
          .LogScriptOutput()
          .WithTransaction()
          .Build();

      if (!runner.IsUpgradeRequired())
      {
        Console.WriteLine("No upgrade required");
        return;
      }

      var result = runner.PerformUpgrade();

      if (!result.Successful)
      {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine(result.Error);
        Console.ResetColor();
      }
      else
      {
        Console.WriteLine("Scripts deployed successfully");
      }
    }
  }
}