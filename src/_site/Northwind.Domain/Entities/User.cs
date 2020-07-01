namespace Northwind.Domain.Entities
{
  using System.Collections.Generic;

  public class User
  {
    public ICollection<UserRole> UserRoles { get; private set; }
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public User()
    {
      UserRoles = new HashSet<UserRole>();  
    }
    
  }
}