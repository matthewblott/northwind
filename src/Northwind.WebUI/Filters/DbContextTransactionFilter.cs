namespace Northwind.WebUI.Filters
{
  using System;
  using System.Threading.Tasks;
  using Domain;
  using Microsoft.AspNetCore.Mvc.Filters;

  public class DbContextTransactionFilter : IAsyncActionFilter
  {
    private readonly NorthwindDbContext _db;

    public DbContextTransactionFilter(NorthwindDbContext db)
    {
      _db = db;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      try
      {
        await _db.BeginTransactionAsync();

        var actionExecuted = await next();
        
        if (actionExecuted.Exception != null && !actionExecuted.ExceptionHandled)
        {
          _db.RollbackTransaction();
        }
        else
        {
          await _db.CommitTransactionAsync();
        }
      }
      catch (Exception)
      {
        _db.RollbackTransaction();
        throw;
      }
    }
  }
}