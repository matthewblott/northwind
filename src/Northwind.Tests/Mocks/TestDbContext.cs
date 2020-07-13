namespace Northwind.Tests.Mocks
{
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  using Application.Common.Interfaces;
  using Domain;
  using Domain.Entities;
  using Microsoft.EntityFrameworkCore;
  using MockQueryable.FakeItEasy;
  using Persistence;

  public class TestDbContext : INorthwindDbContext, IDbContextTransaction
  {
    public TestDbContext()
    {
      const int defaultCount = 50;
      
      Customers = DataMocks.GetCustomerList(defaultCount).AsQueryable().BuildMockDbSet();
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
    public DbSet<Role> Groups { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Region> Region { get; set; }
    public DbSet<Shipper> Shippers { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Territory> Territories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserGroups { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
      return Task.FromResult(0);
    }

    public Task BeginTransactionAsync()
    {
      return Task.CompletedTask;
    }

    public Task CommitTransactionAsync()
    {
      return Task.CompletedTask;
    }

    public void RollbackTransaction()
    {
      // Do nothing
    }
    
  }
}