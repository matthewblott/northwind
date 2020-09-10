namespace Northwind.Application.Products.Commands
{
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using Common.Interfaces;
  using Domain.Common;
  using Domain.Entities;
  using FluentValidation;
  using MediatR;

  public class Create
  {
    public class Command : IRequest<Id>
    {
      [IgnoreMap]
      public string ProductName { get; set; }
      public int? SupplierId { get; set; }
      public int? CategoryId { get; set; }
      public string QuantityPerUnit { get; set; }
      public decimal? UnitPrice { get; set; }
      public short? UnitsInStock { get; set; }
      public short? UnitsOnOrder { get; set; }
      public short? ReorderLevel { get; set; }
      public bool Discontinued { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(v => v.ProductName) ...
      }
    }
    
    public class Handler : IRequestHandler<Command, Id>
    {
      private readonly INorthwindDbContext _db;

      public Handler(INorthwindDbContext db) => _db = db;

      public async Task<Id> Handle(Command command, CancellationToken token)
      {
        var entity = new Product
        {
          ProductName = command.ProductName,
          SupplierId = command.SupplierId,
          CategoryId = command.CategoryId,
          UnitPrice = command.UnitPrice,
          UnitsInStock = 0,
          UnitsOnOrder = 0,
          ReorderLevel = 0,
          Discontinued = command.Discontinued,
        };

        await _db.Products.AddAsync(entity, token);
        await _db.SaveChangesAsync(token);

        return entity.ProductId;

      }
      
    }
    
  }
  
}