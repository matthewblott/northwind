namespace Northwind.WebUI.Common
{
  using Decorators;
  using JetBrains.Annotations;
  using Microsoft.AspNetCore.Mvc;

  public static class ControllerExtensions
  {
    public static RedirectToActionResult SafeRedirect(
      this ControllerBase controller, [AspMvcController] string controllerName)
      => controller.RedirectToAction("Index", controllerName);

    public static RedirectToActionResult SafeRedirect(
      this ControllerBase controller, [AspMvcAction] string actionName, [AspMvcController] string controllerName)
      => controller.RedirectToAction(actionName, controllerName);

      public static IActionResult WithMessage(this IActionResult result, string message) 
        => new MessageDecoratorResult(result, message);
  }

}