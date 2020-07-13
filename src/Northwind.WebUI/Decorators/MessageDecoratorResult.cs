namespace Northwind.WebUI.Decorators
{
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.ViewFeatures;
  using Microsoft.Extensions.DependencyInjection;

  public class MessageDecoratorResult : IActionResult
  {
    public IActionResult Result { get; }
    public string Message { get; }

    public MessageDecoratorResult(IActionResult result, string message)
    {
      Result = result;
      Message = message;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
      var factory = context.HttpContext.RequestServices.GetService<ITempDataDictionaryFactory>();
      var tempData = factory.GetTempData(context.HttpContext);
      
      tempData["_message"] = Message;

      await Result.ExecuteResultAsync(context);
      
    }
    
  }

}
