namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class CategoryConfiguration : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
      builder.Property(e => e.CategoryId);
      
      builder.Property(e => e.CategoryName)
        .IsRequired()
        .HasMaxLength(15);

      builder.Property(e => e.Description);

    }
    
  }
  
}