namespace Northwind.WebUI.Features.Info
{
  using System;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;

  // [AllowAnonymous]
  public class InfoController : Controller
  {
    public string OS() => 
      User?.Identity.IsAuthenticated ?? false ? Environment.OSVersion.ToString() : string.Empty;
  }
}