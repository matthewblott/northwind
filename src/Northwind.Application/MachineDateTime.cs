using System;
using Northwind.Common;

namespace Northwind.Application
{
  public class MachineDateTime : IDateTime
  {
    public DateTime Now => DateTime.Now;

    public int CurrentYear => DateTime.Now.Year;
  }

}