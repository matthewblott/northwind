namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class ProductConfiguration : IEntityTypeConfiguration<Product>
  {
    public void Configure(EntityTypeBuilder<Product> builder)
    {
      builder.Property(e => e.ProductId).HasColumnName("ProductId");
      builder.Property(e => e.CategoryId).HasColumnName("CategoryId");
      builder.Property(e => e.ProductName).IsRequired().HasMaxLength(40);
      builder.Property(e => e.QuantityPerUnit).HasMaxLength(20);
      builder.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");
      builder.Property(e => e.SupplierId).HasColumnName("SupplierId");
      builder.Property(e => e.UnitPrice).HasColumnType("money").HasDefaultValueSql("((0))");
      builder.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");
      builder.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");
      
    }
  }
}