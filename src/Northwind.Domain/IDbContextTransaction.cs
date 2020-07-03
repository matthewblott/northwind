namespace Northwind.Domain
{
  using System.Threading.Tasks;

  public interface IDbContextTransaction
  {
    string ConnectionString { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    void RollbackTransaction();
  }
}