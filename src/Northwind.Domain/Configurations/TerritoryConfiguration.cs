﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Entities;

namespace Northwind.Domain.Configurations
{
  public class TerritoryConfiguration : IEntityTypeConfiguration<Territory>
  {
    public void Configure(EntityTypeBuilder<Territory> builder)
    {
      builder.HasKey(e => e.TerritoryId);

      builder.Property(e => e.TerritoryId)
        .HasColumnName("TerritoryID")
        .HasMaxLength(20)
        .ValueGeneratedNever();

      builder.Property(e => e.RegionId).HasColumnName("RegionID");

      builder.Property(e => e.TerritoryDescription)
        .IsRequired()
        .HasMaxLength(50);

      builder.HasOne(d => d.Region)
        .WithMany(p => p.Territories)
        .HasForeignKey(d => d.RegionId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_Territories_Region");
    }
  }
}