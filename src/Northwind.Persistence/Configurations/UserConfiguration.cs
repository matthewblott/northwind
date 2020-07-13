namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.Property(e => e.Id);
      builder.Property(e => e.Username).IsRequired().HasMaxLength(50);
      builder.Property(e => e.Password).IsRequired().HasMaxLength(50);

      builder
        .HasMany(e => e.UserRoles)
        .WithOne(e => e.User);

    }
    
  }
  
}