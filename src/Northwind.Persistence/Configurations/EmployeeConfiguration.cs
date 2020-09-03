namespace Northwind.Persistence.Configurations
{
  using Domain.Entities;
  using Domain.Types;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;
  using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

  public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
  {
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
      builder.Property(e => e.EmployeeId);
      builder.Property(e => e.Address).HasMaxLength(60);
      builder.Property(e => e.BirthDate);
      builder.Property(e => e.City).HasMaxLength(15);
      builder.Property(e => e.Country).HasMaxLength(15);
      builder.Property(e => e.Extension).HasMaxLength(4);
      builder.Property(e => e.FirstName).IsRequired().HasMaxLength(10);
      
      builder.Property(e => e.Gender)
        .HasColumnName("Gender")
        .HasConversion(new EnumToNumberConverter<Gender, int>());
      
      builder.Property(e => e.HireDate);
      builder.Property(e => e.HomePhone).HasMaxLength(24);
      builder.Property(e => e.LastName).IsRequired().HasMaxLength(20);
      builder.Property(e => e.Notes);
      builder.Property(e => e.Photo);
      builder.Property(e => e.PhotoPath).HasMaxLength(255);
      builder.Property(e => e.PostalCode).HasMaxLength(10);
      builder.Property(e => e.Region).HasMaxLength(15);
      
      builder.Property(e => e.BirthDate)
        .HasConversion(new DateTimeToStringConverter());

      
      
      builder.Property(e => e.Title).HasMaxLength(30);
      builder.Property(e => e.TitleOfCourtesy).HasMaxLength(25);

      builder.HasOne(d => d.Manager)
        .WithMany(p => p.DirectReports)
        .HasForeignKey(d => d.ReportsTo);
     
      
      
    }
  }
}