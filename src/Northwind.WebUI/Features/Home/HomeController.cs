namespace Northwind.WebUI.Features.Home
{
  using Microsoft.AspNetCore.Mvc;

  public class HomeController : Controller
  {
    public IActionResult Index() => View();
  }
  
}