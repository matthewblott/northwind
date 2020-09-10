namespace Northwind.WebUI.Features.Products
{
  using System;
  using System.Linq;
  using Application.Products.Commands;
  using Application.Products.Queries;
  using CsvHelper.Configuration.Attributes;
  using Domain.Common;
  using Microsoft.AspNetCore.Mvc.Filters;

  public class MyActionFilterAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      // context.HttpContext.Request.Method
      // context.HttpContext.Request.HasFormContentType
      
      var value = context.ActionArguments.First().Value;

      switch (value)
      {
        case Update.Command command:
        {
          var values = context.HttpContext.Request.Form.Single(x 
            => string.Equals(x.Key, nameof(Id), StringComparison.CurrentCultureIgnoreCase)).Value;
          
          var id = Convert.ToInt32(values.First());

          command!.Id = new Id(id);
          break;
        }
        case Details.Query query:
        {
          var any = context.RouteData.Values.Any(x 
            => string.Equals(x.Key, nameof(Id), StringComparison.CurrentCultureIgnoreCase));

          if (any)
          {
            var id = Convert.ToInt32(context.RouteData.Values[nameof(Id).ToLower()]);
          
            query!.Id = new Id(id);
            
          }
          
          break;
        }
        
      }

    }
    
  }

}