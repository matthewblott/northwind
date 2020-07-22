namespace Northwind.Infrastructure.Files
{
  using Application.Products.Commands;
  using CsvHelper.Configuration;

  public sealed class ProductFileRecordMap : ClassMap<Import.Model>
  {
    public ProductFileRecordMap()
    {
      // Map(m => m.ProductId).Name("product_id");
      Map(m => m.ProductName).Name("product_name");
      Map(m => m.SupplierId).Name("supplier_id");
      Map(m => m.CategoryId).Name("category_id");
      Map(m => m.QuantityPerUnit).Name("quantity_per_unit");
      Map(m => m.UnitPrice).Name("unit_price");
      Map(m => m.UnitsInStock).Name("units_in_stock");
      Map(m => m.UnitsOnOrder).Name("units_on_order");
      Map(m => m.ReorderLevel).Name("reorder_level");
      Map(m => m.Discontinued).Name("discontinued");
    }
    
  }
  
}