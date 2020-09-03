namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
  {
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
      builder.HasKey(e => new {e.OrderId, e.ProductId});
      builder.Property(e => e.OrderId);
      builder.Property(e => e.ProductId);
      builder.Property(e => e.Quantity);
      builder.Property(e => e.UnitPrice);
      builder.HasOne(d => d.Order)
        .WithMany(p => p.OrderDetails)
        .HasForeignKey(d => d.OrderId)
        .OnDelete(DeleteBehavior.ClientSetNull);
      builder.HasOne(d => d.Product)
        .WithMany(p => p.OrderDetails)
        .HasForeignKey(d => d.ProductId)
        .OnDelete(DeleteBehavior.ClientSetNull);
    }
  }
}