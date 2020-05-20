using System;
using Northwind.Common;

namespace Northwind.WebUI.Infrastructure
{
  public class MachineDateTime : IDateTime
  {
    public DateTime Now => DateTime.Now;

    public int CurrentYear => DateTime.Now.Year;
  }

}