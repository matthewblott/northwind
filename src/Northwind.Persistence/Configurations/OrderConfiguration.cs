﻿namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class OrderConfiguration : IEntityTypeConfiguration<Order>
  {
    public void Configure(EntityTypeBuilder<Order> builder)
    {
      builder.Property(e => e.OrderId).HasColumnName("OrderId");

      builder.Property(e => e.CustomerId)
        .HasColumnName("CustomerId")
        .HasMaxLength(5);

      builder.Property(e => e.EmployeeId).HasColumnName("EmployeeId");

      builder.Property(e => e.Freight)
        .HasColumnType("money")
        .HasDefaultValueSql("((0))");

      builder.Property(e => e.OrderDate).HasColumnType("datetime");

      builder.Property(e => e.RequiredDate).HasColumnType("datetime");

      builder.Property(e => e.ShipAddress).HasMaxLength(60);

      builder.Property(e => e.ShipCity).HasMaxLength(15);

      builder.Property(e => e.ShipCountry).HasMaxLength(15);

      builder.Property(e => e.ShipName).HasMaxLength(40);

      builder.Property(e => e.ShipPostalCode).HasMaxLength(10);

      builder.Property(e => e.ShipRegion).HasMaxLength(15);

      builder.Property(e => e.ShippedDate).HasColumnType("datetime");

      builder.HasOne(d => d.Shipper)
        .WithMany(p => p.Orders)
        .HasForeignKey(d => d.ShipVia)
        .HasConstraintName("FK_Orders_Shippers");
      
    }
  }
}