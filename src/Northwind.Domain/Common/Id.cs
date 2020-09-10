namespace Northwind.Domain.Common
{
  public class Id
  {
    private readonly int _id;
    public Id(int id) => _id = id;
    public static implicit operator int(Id d) => d._id;
    public static explicit operator Id(int b) => new Id(b);
    public override string ToString() => $"{_id}";
  }
  
}