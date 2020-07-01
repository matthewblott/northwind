namespace Northwind.WebUI.Infrastructure
{
  using System;
  using Common;
  
  public class MachineDateTime : IDateTime
  {
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
    
  }

}