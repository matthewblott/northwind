namespace Northwind.WebUI.Features.Account
{
  using System.ComponentModel.DataAnnotations;

  public class LoginViewModel
  {
    public string Username { get; set; }
    
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}