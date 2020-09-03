namespace Northwind.Common
{
  public interface ICurrentUserService
  {
    string UserId { get; }

    bool IsAuthenticated { get; }
  }
}