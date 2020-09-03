namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class ProductConfiguration : IEntityTypeConfiguration<Product>
  {
    public void Configure(EntityTypeBuilder<Product> builder)
    {
      builder.Property(e => e.ProductId);
      builder.Property(e => e.CategoryId);
      builder.Property(e => e.ProductName).IsRequired().HasMaxLength(40);
      builder.Property(e => e.QuantityPerUnit).HasMaxLength(20);
      builder.Property(e => e.ReorderLevel);
      builder.Property(e => e.SupplierId);
      builder.Property(e => e.UnitPrice);
      builder.Property(e => e.UnitsInStock);
      builder.Property(e => e.UnitsOnOrder);
      
    }
  }
}