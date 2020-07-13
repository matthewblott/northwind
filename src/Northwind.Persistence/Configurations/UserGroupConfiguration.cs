namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class UserGroupConfiguration : IEntityTypeConfiguration<UserRole>
  {
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
      builder.HasKey(e => new { e.UserId, GroupId = e.RoleId });
      builder.Property(e => e.UserId).IsRequired();
      builder.Property(e => e.RoleId).IsRequired();
      
      builder
        .HasOne(e => e.User)
        .WithMany(e => e.UserRoles)
        .HasForeignKey(e => e.UserId);

      builder.HasOne(e => e.Role);
      
    }

  }
  
}