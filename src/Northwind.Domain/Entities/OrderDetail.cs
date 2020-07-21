namespace Northwind.Domain.Entities
{
  using Common;

  public class OrderDetail : AuditableEntity
  {
    public Order Order { get; set; }
    public Product Product { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
    public float Discount { get; set; }

  }
}