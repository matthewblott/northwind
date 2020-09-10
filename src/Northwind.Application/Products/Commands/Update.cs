namespace Northwind.Application.Products.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using Common.Interfaces;
  using Domain.Common;
  using FluentValidation;
  using MediatR;

  public class Update
  {
    public class Command : IRequest<Unit>
    {
      [IgnoreMap]
      public Id Id { get; set; }
      public string ProductName { get; set; }
      public decimal? UnitPrice { get; set; }
      public int? SupplierId { get; set; }
      public int? CategoryId { get; set; }
      public bool Discontinued { get; set; }
    }

    // Validator
    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(v => v.ProductName).NotEmpty();
      }
    }
    
    // Handler
    public class Handler : IRequestHandler<Command>
    {
      private readonly INorthwindDbContext _db;

      public Handler(INorthwindDbContext db)
      {
        _db = db;
      }

      public async Task<Unit> Handle(Command command, CancellationToken token)
      {
        var entity = await _db.Products.FindAsync(command.Id);

        entity.ProductName = command.ProductName;
        entity.CategoryId = command.CategoryId;
        entity.SupplierId = command.SupplierId;
        entity.UnitPrice = command.UnitPrice;
        entity.Discontinued = command.Discontinued;

        await _db.SaveChangesAsync(token);

        return Unit.Value;
        
      }
      
    }

  }
  
}