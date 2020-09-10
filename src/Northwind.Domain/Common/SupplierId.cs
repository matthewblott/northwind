namespace Northwind.Domain.Common
{
  using System;

  public readonly struct SupplierId : IComparable<SupplierId>, IEquatable<SupplierId>
  {
    private int Value { get; }

    public SupplierId(int value) => Value = value;

    // public static SupplierId New() => new SupplierId(Guid.NewGuid());

    public bool Equals(SupplierId other) => Value.Equals(other.Value);
    public int CompareTo(SupplierId other) => Value.CompareTo(other.Value);

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj))
      {
        return false;
      }
      
      return obj is SupplierId other && Equals(other);
      
    }

    public static implicit operator int(SupplierId value) => value.Value;
      public static explicit operator SupplierId(int value) => new SupplierId(value);
    
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => $"{Value}";

    public static bool operator ==(SupplierId a, SupplierId b) => a.CompareTo(b) == 0;
    public static bool operator !=(SupplierId a, SupplierId b) => !(a == b);
  }

}