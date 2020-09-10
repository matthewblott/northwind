namespace Northwind.Persistence.Mappings
{
  using System;
  using System.Threading;
  using Domain.Common;
  using Microsoft.EntityFrameworkCore.ChangeTracking;
  using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

  public class TemporaryIdValueGenerator : TemporaryNumberValueGenerator<Id>
  {
    private int _current = int.MinValue + 1000;

    public override Id Next(EntityEntry entry)
    {
      var id = Interlocked.Increment(ref _current);
      
      return new Id(id);
    }
  }
  
}