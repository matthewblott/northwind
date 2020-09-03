namespace Northwind.Application.Employees.Commands
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
    // Command
    public class Command : IRequest<Unit>
    {
      [IgnoreMap]
      public int? Id { get; set; }
      public string Title { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public DateTime? BirthDate { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      public string Region { get; set; }
      public string PostalCode { get; set; }
      public string Country { get; set; }
      public string HomePhone { get; set; }
      public string Position { get; set; }
      public string Extension { get; set; }
      public DateTime? HireDate { get; set; }
      public string Notes { get; set; }
      public byte[] Photo { get; set; }
      public int? ManagerId { get; set; }
    }
    
    // Validator
    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(v => v.Id).NotEmpty();
        RuleFor(e => e.FirstName).Length(10).NotEmpty();
        RuleFor(e => e.LastName).Length(20).NotEmpty();
        RuleFor(e => e.Address).Length(60);
        RuleFor(e => e.BirthDate);
        RuleFor(e => e.City).Length(15);
        RuleFor(e => e.Country).Length(15);
        RuleFor(e => e.Extension).Length(4);
        RuleFor(e => e.HireDate);
        RuleFor(e => e.HomePhone).Length(24);
        RuleFor(e => e.Notes);
        RuleFor(e => e.Photo);
        RuleFor(e => e.PostalCode).Length(10);
        RuleFor(e => e.Region).Length(15);
        RuleFor(e => e.Position).Length(30);
        RuleFor(e => e.Title).Length(25);
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

        var entity = await _db.Employees.FindAsync(command.Id.Value);

        entity.TitleOfCourtesy = command.Title;
        entity.FirstName = command.FirstName;
        entity.LastName = command.LastName;
        entity.BirthDate = command.BirthDate;
        entity.Address = command.Address;
        entity.City = command.City;
        entity.Region = command.Region;
        entity.PostalCode = command.PostalCode;
        entity.Country = command.Country;
        entity.HomePhone = command.HomePhone;
        entity.Title = command.Position;
        entity.Extension = command.Extension;
        entity.HireDate = command.HireDate;
        entity.Notes = command.Notes;
        entity.ReportsTo = command.ManagerId;
        
        await _db.SaveChangesAsync(token);

        return Unit.Value;
        
      }
      
    }

  }
  
}