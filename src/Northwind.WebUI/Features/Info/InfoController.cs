namespace Northwind.WebUI.Features.Info
{
  using System;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;

  [AllowAnonymous]
  public class InfoController : Controller
  {
    public string Platform() =>
      Environment.OSVersion.Platform switch
      {
        PlatformID.MacOSX => nameof(PlatformID.MacOSX),
        PlatformID.Unix => nameof(PlatformID.Unix),
        PlatformID.Win32S => nameof(PlatformID.Win32S),
        PlatformID.Win32Windows => nameof(PlatformID.Win32Windows),
        PlatformID.Win32NT => nameof(PlatformID.Win32NT),
        PlatformID.WinCE => nameof(PlatformID.WinCE),
        PlatformID.Xbox => nameof(PlatformID.Xbox),
        _ => throw new ArgumentOutOfRangeException()
      };
    
  }
  
}