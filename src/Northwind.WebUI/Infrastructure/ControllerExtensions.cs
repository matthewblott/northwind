namespace Northwind.WebUI.Infrastructure
{
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
    
  }

}