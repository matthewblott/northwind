namespace Northwind.Domain.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;
  using Entities;
  
  public class CategoryConfiguration : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
      builder.Property(e => e.CategoryId).HasColumnName("CategoryId");
      
      builder.Property(e => e.CategoryName)
        .IsRequired()
        .HasMaxLength(15);
      
      builder.Property(e => e.Description).HasColumnType("ntext");
      
    }
    
  }
  
}