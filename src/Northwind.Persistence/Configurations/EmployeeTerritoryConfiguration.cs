namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class EmployeeTerritoryConfiguration : IEntityTypeConfiguration<EmployeeTerritory>
  {
    public void Configure(EntityTypeBuilder<EmployeeTerritory> builder)
    {
      builder.HasKey(e => new {e.EmployeeId, e.TerritoryId});
      builder.Property(e => e.EmployeeId);
      builder.Property(e => e.TerritoryId).HasMaxLength(20);

      builder.HasOne(d => d.Employee)
        .WithMany(p => p.EmployeeTerritories)
        .HasForeignKey(d => d.EmployeeId)
        .OnDelete(DeleteBehavior.ClientSetNull);

      builder.HasOne(d => d.Territory)
        .WithMany(p => p.EmployeeTerritories)
        .HasForeignKey(d => d.TerritoryId)
        .OnDelete(DeleteBehavior.ClientSetNull);
    }
  }
}