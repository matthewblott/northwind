namespace Northwind.Domain
{
  using Microsoft.EntityFrameworkCore;
  using Entities;
  using System.Threading;
  using System.Threading.Tasks;
  
  public interface INorthwindDbContext
  {
    DbSet<Category> Categories { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
    DbSet<Role> Groups { get; set; }
    DbSet<OrderDetail> OrderDetails { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Region> Region { get; set; }
    DbSet<Shipper> Shippers { get; set; }
    DbSet<Supplier> Suppliers { get; set; }
    DbSet<Territory> Territories { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<UserRole> UserGroups { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

  }
  
}