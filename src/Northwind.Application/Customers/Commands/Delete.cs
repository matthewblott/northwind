namespace Northwind.Application.Customers.Commands
{
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  using Domain;
  using FluentValidation;
  using MediatR;

  public class Delete
  {
    // Query
    
    // Validator
    
    // Handler
    
    // Command
    public class Command : IRequest
    {
      public string Id { get; set; }
    }
    
    public class CommandValidator : AbstractValidator<Command>
    {
      public CommandValidator()
      {
        RuleFor(v => v.Id).NotEmpty().Length(5);
      }
    }
    
    // Handler
    public class CommandHandler : IRequestHandler<Command>
    {
      private readonly INorthwindDbContext _db;

      public CommandHandler(INorthwindDbContext db)
      {
        _db = db;
      }

      public async Task<Unit> Handle(Command command, CancellationToken token)
      {
        var entity = await _db.Customers
          .FindAsync(command.Id);

        // if (entity == null)
        // {
        //   throw new NotFoundException(nameof(Customer), command.Id);
        // }

        var hasOrders = _db.Orders.Any(o => o.CustomerId == entity.CustomerId);
        
        if (hasOrders)
        {
          // throw new DeleteFailureException(nameof(Customer), command.Id,
          //   "There are existing orders associated with this customer.");
        }

        _db.Customers.Remove(entity);

        await _db.SaveChangesAsync(token);

        return Unit.Value;
      }
    }
    
  }
  
}