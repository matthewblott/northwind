namespace Northwind.Application.Categories.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using Common.Interfaces;
  using FluentValidation;
  using MediatR;

  public class Update
  {
    public class Command : IRequest<Unit>
    {
      [IgnoreMap]
      public int? Id { get; set; }
    }

    // Validator
    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(v => v.Id) ...
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
        if (!command.Id.HasValue)
        {
          throw new NullReferenceException();
        }

        var entity = await _db.Categories.FindAsync(command.Id.Value);

        // entity.Photo = command.Photo;
        
        await _db.SaveChangesAsync(token);

        return Unit.Value;
        
      }
      
    }

  }
  
}