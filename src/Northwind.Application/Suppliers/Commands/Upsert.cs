namespace Northwind.Application.Suppliers.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using Common.Interfaces;
  using Domain.Entities;
  using FluentValidation;
  using MediatR;

  public class Upsert
  {
    public class Query : IRequest<Command>
    {
      public int? Id { get; set; }
    }
    
    public class QueryValidator : AbstractValidator<Query>
    {
      public QueryValidator()
      {
        RuleFor(m => m.Id).NotNull();
      }
    }
    
    public class Command : IRequest<int>
    {
      [IgnoreMap] public int? Id { get; set; }
      public string CompanyName { get; set; }
      public string ContactName { get; set; }
      public string ContactTitle { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      public string Region { get; set; }
      public string PostalCode { get; set; }
      public string Country { get; set; }
      public string Phone { get; set; }
      public string Fax { get; set; }
      public string HomePage { get; set; }

    }

    public class CommandValidator : AbstractValidator<Command>
    {
      public CommandValidator()
      {
        // RuleFor(v => v.FirstName).MaximumLength(50).NotEmpty();
        // RuleFor(v => v.LastName).MaximumLength(50).NotEmpty();
        // RuleFor(v => v.BirthDate).NotEmpty();
        // RuleFor(v => v.HireDate).NotEmpty();
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
        Supplier entity;
        
        if (command.Id.HasValue)
        {
          entity = await _db.Suppliers.FindAsync(command.Id.Value);
        }
        else
        {
          entity = new Supplier();

          await _db.Suppliers.AddAsync(entity, token);
        }
        
        entity.CompanyName = command.CompanyName;
        
        await _db.SaveChangesAsync(token);
        
        return entity.SupplierId;

      }

    }

  }
  
}