namespace Northwind.Domain.Configurations
{
  using Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class GroupConfiguration : IEntityTypeConfiguration<Role>
  {
    public void Configure(EntityTypeBuilder<Role> builder)
    {
      builder.Property(e => e.Id);
      builder.Property(e => e.Name).IsRequired().HasMaxLength(50);;
    }
  }
}