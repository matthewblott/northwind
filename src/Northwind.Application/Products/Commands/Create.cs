namespace Northwind.Application.Products.Commands
{
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using Domain;
  using Domain.Entities;
  using FluentValidation;
  using MediatR;

  public class Create
  {
    public class Command : IRequest<int>
    {
      [IgnoreMap]
      public int? Id { get; set; }
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
        // RuleFor(v => v.Id) ...
      }
    }
    
    public class Handler : IRequestHandler<Command, int>
    {
      private readonly INorthwindDbContext _db;
      private readonly IMapper _mapper;

      public Handler(INorthwindDbContext db, IMapper mapper)
      {
        _db = db;
        _mapper = mapper;
      }

      public async Task<int> Handle(Command command, CancellationToken token)
      {
        var entity = new Product
        {
          ProductName = command.ProductName,
          CategoryId = command.CategoryId,
          SupplierId = command.SupplierId,
          UnitPrice = command.UnitPrice,
          Discontinued = command.Discontinued
        };

        await _db.Products.AddAsync(entity, token);
        await _db.SaveChangesAsync(token);

        return entity.ProductId;

      }
      
    }
    
  }
  
}