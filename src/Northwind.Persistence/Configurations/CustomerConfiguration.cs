﻿namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
  {
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
      builder.Property(e => e.CustomerId)
        .HasConversion(
          p => p, 
          s => s.ToUpper())
        .HasMaxLength(5)
        .ValueGeneratedNever();

      builder.Property(e => e.Address).HasMaxLength(60);

      builder.Property(e => e.City).HasMaxLength(15);
      
      const int companyNameLength = 9;
      
      builder.Property(e => e.CompanyName)
        .IsRequired()
        .HasMaxLength(companyNameLength);

      builder.Property(e => e.ContactName).HasMaxLength(30);

      builder.Property(e => e.ContactTitle).HasMaxLength(30);

      builder.Property(e => e.Country).HasMaxLength(15);

      builder.Property(e => e.Fax).HasMaxLength(24);

      builder.Property(e => e.Phone).HasMaxLength(24);

      builder.Property(e => e.PostalCode).HasMaxLength(10);

      builder.Property(e => e.Region).HasMaxLength(15);
    }
  }
}