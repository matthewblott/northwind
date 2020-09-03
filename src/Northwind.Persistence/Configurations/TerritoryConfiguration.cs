namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class TerritoryConfiguration : IEntityTypeConfiguration<Territory>
  {
    public void Configure(EntityTypeBuilder<Territory> builder)
    {
      builder.HasKey(e => e.TerritoryId);
      builder.Property(e => e.TerritoryId).HasMaxLength(20).ValueGeneratedNever();
      builder.Property(e => e.RegionId);
      builder.Property(e => e.TerritoryDescription).IsRequired().HasMaxLength(50);
      builder.HasOne(d => d.Region)
        .WithMany(p => p.Territories)
        .HasForeignKey(d => d.RegionId)
        .OnDelete(DeleteBehavior.ClientSetNull);
      
    }
    
  }
  
}