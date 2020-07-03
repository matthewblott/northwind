namespace Northwind.Application.Categories.Commands
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
      public string CategoryName { get; set; }
      public string Description { get; set; }

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
        var entity = new Category
        {
        };

        await _db.Categories.AddAsync(entity, token);
        await _db.SaveChangesAsync(token);

        return entity.CategoryId;

      }
      
    }
    
  }
  
}