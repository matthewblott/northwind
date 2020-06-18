namespace Northwind.Application.Categories.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using Common.Interfaces;
  using FluentValidation;
  using MediatR;

  public class Import
  {
    public class Command : IRequest<Unit>
    {
      [IgnoreMap]
      public string Id { get; set; }

    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
      }
    }
    
    public class Handler : IRequestHandler<Command, Unit>
    {
      private readonly INorthwindDbContext _db;
      private readonly IMapper _mapper;

      public Handler(INorthwindDbContext db, IMapper mapper)
      {
        _db = db;
        _mapper = mapper;
      }

      // See
      // https://www.howtogeek.com/405468/how-to-perform-a-task-when-a-new-file-is-added-to-a-directory-in-linux/
      
      public async Task<Unit> Handle(Command command, CancellationToken token)
      {
        // var file = new Mono.Posix.UnixFileInfo("test.txt");
        // var file = new Mono.Unix.UnixFileInfo("");
        // file.SetOwner("");
        
        throw new NotImplementedException();
      }
      
    }

  }
  
}