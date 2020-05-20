namespace Northwind.Application.Categories.Commands.DeleteCategory
{
  using MediatR;
  using Common.Interfaces;
  using System.Threading;
  using System.Threading.Tasks;

  public class DeleteCategoryCommand : IRequest
  {
    public int Id { get; set; }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
      private readonly INorthwindDbContext _context;

      public DeleteCategoryCommandHandler(INorthwindDbContext context)
      {
        _context = context;
      }

      public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
      {
        var entity = await _context.Categories
          .FindAsync(request.Id);

        if (entity == null)
        {
          // throw new NotFoundException(nameof(Category), request.Id);
        }

        _context.Categories.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
      }
    }
  }
}