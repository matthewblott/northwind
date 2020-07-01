namespace Northwind.Domain
{
  using System;
  using System.Data;
  using System.Linq;
  using System.Reflection;
  using System.Threading;
  using System.Threading.Tasks;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Storage;
  using Northwind.Application.Common.Interfaces;
  using Northwind.Common;
  using Common;
  using Entities;
  using FluentValidation;
  using FluentValidation.Results;

  public class NorthwindDbContext : DbContext, INorthwindDbContext
  {
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
    private IDbContextTransaction _currentTransaction;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options)
    {
    }

    public NorthwindDbContext(
      DbContextOptions<NorthwindDbContext> options,
      ICurrentUserService currentUserService,
      IDateTime dateTime)
      : base(options)
    {
      _currentUserService = currentUserService;
      _dateTime = dateTime;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      // Validation
      foreach (var entry in ChangeTracker.Entries())
      {
        var vt = typeof (AbstractValidator<>);
        var evt = vt.MakeGenericType(entry.Entity.GetType());  // entry.Metadata.Name
        var validatorTypes =
          Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(evt)).ToList();

        var failures = validatorTypes
          .Select(vt0 => ((IValidator) 
            Activator.CreateInstance(vt0))?.Validate(entry.Entity) ?? new ValidationResult())
          .SelectMany(result => result.Errors)
          .Where(f => f != null)
          .ToList();

        if (failures.Count != 0)
        {
          throw new ValidationException(failures);
        }
        
        // foreach (var validatorType in validatorTypes)
        // {
        //   var validatorInstance = (IValidator)Activator.CreateInstance(validatorType);
        //
        //   if (validatorInstance == null)
        //   {
        //     continue;
        //   }
        //   
        //   var validationResult = validatorInstance.Validate(entry.Entity);
        //
        //   if (!validationResult.IsValid)
        //   {
        //     throw new ValidationException("Invalid");
        //   }
        //   
        // }
        
        switch (entry.State)
        {
          case EntityState.Added:
            // entry.Entity.CreatedBy = _currentUserService.UserId;
            // entry.Entity.Created = _dateTime.Now;
            break;
          case EntityState.Modified:
            // entry.Entity.LastModifiedBy = _currentUserService.UserId;
            // entry.Entity.LastModified = _dateTime.Now;
          case EntityState.Deleted:
            break;
        }
      }
      
      foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entry.Entity.CreatedBy = _currentUserService.UserId;
            entry.Entity.Created = _dateTime.Now;
            break;
          case EntityState.Modified:
            entry.Entity.LastModifiedBy = _currentUserService.UserId;
            entry.Entity.LastModified = _dateTime.Now;
            break;
        }
      }

      return base.SaveChangesAsync(cancellationToken);
      
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(NorthwindDbContext).Assembly);
    }
    
    public async Task BeginTransactionAsync()
    {
      if (_currentTransaction != null)
      {
        return;
      }

      _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
    }

    public async Task CommitTransactionAsync()
    {
      try
      {
        await SaveChangesAsync().ConfigureAwait(false);

        if (_currentTransaction != null) await _currentTransaction?.CommitAsync();
      }
      catch
      {
        RollbackTransaction();
        throw;
      }
      finally
      {
        if (_currentTransaction != null)
        {
          _currentTransaction.Dispose();
          _currentTransaction = null;
        }
      }
    }

    public void RollbackTransaction()
    {
      try
      {
        _currentTransaction?.Rollback();
      }
      finally
      {
        if (_currentTransaction != null)
        {
          _currentTransaction.Dispose();
          _currentTransaction = null;
        }
      }
    }
    
  }
}