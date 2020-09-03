namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class RegionConfiguration : IEntityTypeConfiguration<Region>
  {
    public void Configure(EntityTypeBuilder<Region> builder)
    {
      builder.HasKey(e => e.RegionId);
      builder.Property(e => e.RegionId).ValueGeneratedNever();
      builder.Property(e => e.RegionDescription).IsRequired().HasMaxLength(50);
    }
  }
}