namespace Northwind.Tests.Integration.Helpers
{
  using System.IO;
  using System.Reflection;

  public static class Utilities
  {
    public static string GetProjectFolderPath()
    {
      var assembly = Assembly.GetExecutingAssembly();
      var assemblyName = assembly.GetName().Name?.ToLower();
      var info = Directory.GetParent(assembly.Location);
      
      while (info?.Name.ToLower() != assemblyName && info?.Name != Path.DirectorySeparatorChar.ToString())
      {
        info = info?.Parent;
      }

      if (info?.FullName == Path.DirectorySeparatorChar.ToString())
      {
        throw new DirectoryNotFoundException();
      }

      return info?.FullName;

    }

    public static string GetTestDatabaseConnectionString() =>
      "DataSource=" + Path.Combine(GetProjectFolderPath(), "db", "northwind_test.db");

  }
  
}