namespace Northwind.WebUI.Features.Customers
{
  using System.ComponentModel.DataAnnotations;
  using Microsoft.AspNetCore.Mvc;

  public class PostTestViewModel
  {
    // AdditionalFields = "CompanyName,Address,City"
    [Required]
    [StringLength(5)]
    [Remote(action: "IdAvailable", controller:"Customers")]
    public string Id { get; set; }
      
    [Required]
    [Display(Name = "Company", Prompt = "Company")]
    public string CompanyName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    
  }
}