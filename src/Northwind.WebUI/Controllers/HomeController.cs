namespace Northwind.WebUI.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  public class HomeController : Controller
  {
    public IActionResult Index() => View();
  }
  
}