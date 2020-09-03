namespace Northwind.Tests.Integration.Helpers
{
  using System;
  using System.IO;
  using System.Reflection;

  public static class Utilities
  {
    private static string GetProjectFolderPath()
    {
      var assembly = Assembly.GetExecutingAssembly();
      var assemblyName = assembly.GetName().Name?.ToLower();
      
      DirectoryInfo? info = Directory.GetParent(assembly.Location);
      
      while (info?.Name.ToLower() != assemblyName && info?.Name != Path.DirectorySeparatorChar.ToString())
      {
        info = info?.Parent;
      }

      if (info?.FullName == Path.DirectorySeparatorChar.ToString())
      {
        throw new DirectoryNotFoundException();
      }

      if (info == null)
      {
        throw new NullReferenceException();
      }
      
      return info.FullName;

    }

    public static string GetTestDatabaseConnectionString() =>
      "DataSource=" + Path.Combine(GetProjectFolderPath(), "db", "northwind_test.sqlite");

    public static string GetProductFilePath() => Path.Combine(GetProjectFolderPath(), "db", "products.csv");

  }
  
}