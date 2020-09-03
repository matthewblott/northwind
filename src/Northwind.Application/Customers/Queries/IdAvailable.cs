namespace Northwind.Application.Customers.Queries
{
  using System.Threading;
  using System.Threading.Tasks;
  using Common.Interfaces;
  using FluentValidation;
  using MediatR;
  using Microsoft.EntityFrameworkCore;

  public class IdAvailable
  {
    public class Query : IRequest<bool>
    {
      public string Id { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
      public Validator()
      {
        RuleFor(x => x.Id).NotEmpty();
      }
    }
    
    public class Handler : IRequestHandler<Query, bool>
    {
      private readonly INorthwindDbContext _db;
    
      public Handler(INorthwindDbContext db) => _db = db;

      public async Task<bool> Handle(Query query, CancellationToken token) 
        => !await _db.Customers.AnyAsync(c => c.CustomerId == query.Id, token);
      
    }

  }
  
}