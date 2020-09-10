namespace Northwind.Persistence.Mappings
{
  using Domain.Common;
  using Microsoft.EntityFrameworkCore.Storage;

  public class IdTypeMappingPlugin : IRelationalTypeMappingSourcePlugin
  {
    public RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo) 
      => (mappingInfo.ClrType == typeof(Id) ? new IdTypeMapping() : null)!;
    
  }
}