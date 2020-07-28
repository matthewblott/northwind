namespace Northwind.WebUI.Filters
{
  using System;
  using System.Threading.Tasks;
  using Application.Common.Interfaces;
  using Microsoft.AspNetCore.Mvc.Filters;

  public class DbContextTransactionFilter : IAsyncActionFilter
  {
    private readonly INorthwindDbContext _db;

    public DbContextTransactionFilter(INorthwindDbContext db)
    {
      _db = db;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      try
      {
        await _db.BeginAsync();

        var actionExecuted = await next();
        
        if (actionExecuted.Exception != null && !actionExecuted.ExceptionHandled)
        {
          await _db.RollbackAsync();
        }
        else
        {
          await _db.CommitAsync();
        }
      }
      catch (Exception)
      {
        await _db.RollbackAsync();
        throw;
      }
    }
  }
}