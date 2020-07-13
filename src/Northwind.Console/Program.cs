namespace Northwind.Console
{
  using Database;

  internal static class Program
  {
    private static void Main(string[] args)
    {
      Migration.Migrate();
    }
    
  }
  
}