namespace Northwind.Tests.Integration.Helpers
{
  using System;
  using System.Data;
  using System.IO;
  using System.Reflection;
  using System.Threading.Tasks;
  using Microsoft.Data.Sqlite;
  using Microsoft.Extensions.FileProviders;

  public static class DataHelper
  {
    private static async Task<string> GetSql(string action)
    {
      var assembly = Assembly.GetExecutingAssembly();
      var provider = new EmbeddedFileProvider(assembly);
      var path = @$"Scripts\{action}.sql";

      var names = assembly.GetManifestResourceNames();
      
      foreach (var name in names)
      {
        Console.WriteLine(name);
      }
      
      await using var stream = provider.GetFileInfo(path).CreateReadStream();
      
      var reader = new StreamReader(stream);
      var sql = await reader.ReadToEndAsync();
        
      return sql;
    }
    
    public static async Task Configure()
    {
      static async Task<int> Execute(string action, SqliteTransaction tr)
      {
        var sql = GetSql(action).Result;
        var cmd = new SqliteCommand { Connection = tr.Connection, CommandText = sql, Transaction = tr };
        return await cmd.ExecuteNonQueryAsync();
      }

      await using var conn = new SqliteConnection(Utilities.GetTestDatabaseConnectionString());

      await conn.OpenAsync();

      var trans = conn.BeginTransaction();

      try
      {
        if (Execute("01_tables_drop", trans).Result > 0)
        {
          throw new Exception();
        }
        if (Execute("02_tables_create", trans).Result > 0)
        {
          throw new Exception();
        }

        // if (Execute("3_tables_populate", trans).Result > 1093940)
        // {
        //   throw new Exception();
        // }

        await trans.CommitAsync();
      }
      catch (Exception ex)
      {
        await trans.RollbackAsync();
      }
      finally
      {
        await conn.CloseAsync();
        await conn.DisposeAsync();
        await trans.DisposeAsync();
      }

    }
    
  }
  
}