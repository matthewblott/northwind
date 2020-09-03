namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
  {
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
      builder.Property(e => e.SupplierId).IsRequired();
      builder.Property(e => e.Address).HasMaxLength(60).IsRequired();
      builder.Property(e => e.City).HasMaxLength(15).IsRequired();
      builder.Property(e => e.CompanyName).IsRequired().HasMaxLength(40).IsRequired();
      builder.Property(e => e.ContactName).HasMaxLength(30).IsRequired();
      builder.Property(e => e.ContactTitle).HasMaxLength(30).IsRequired();
      builder.Property(e => e.Country).HasMaxLength(15).IsRequired();
      builder.Property(e => e.Fax).HasMaxLength(24);
      builder.Property(e => e.HomePage);
      builder.Property(e => e.Phone).HasMaxLength(24);
      builder.Property(e => e.PostalCode).HasMaxLength(10).IsRequired();
      builder.Property(e => e.Region).HasMaxLength(15).IsRequired();
    }
  }
}