﻿namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
  {
    public void Configure(EntityTypeBuilder<Shipper> builder)
    {
      builder.Property(e => e.ShipperId);
      builder.Property(e => e.CompanyName).IsRequired().HasMaxLength(40);
      builder.Property(e => e.Phone).HasMaxLength(24);
    }
  }
}