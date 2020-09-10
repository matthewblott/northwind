namespace Northwind.Persistence.Mappings
{
  using System;
  using Domain.Common;
  using Microsoft.EntityFrameworkCore.Storage;
  using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

  public class IdTypeMapping : RelationalTypeMapping
  {
    private static readonly ValueConverter<Id, int> Convert
      = new ValueConverter<Id, int>(v => v,
        v => new Id(v),
        new ConverterMappingHints(valueGeneratorFactory: (p, t) => new TemporaryIdValueGenerator()));

    // See:
    // https://www.thinktecture.com/en/entity-framework-core/improved-value-conversion-support-in-2-1/
    // https://github.com/dotnet/efcore/issues/13814
    
    public IdTypeMapping() : base(new RelationalTypeMappingParameters(
        new CoreTypeMappingParameters(typeof(Id), Convert), "INTEGER")) { }

    protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters) 
      => throw new NotImplementedException();
    
  }

}