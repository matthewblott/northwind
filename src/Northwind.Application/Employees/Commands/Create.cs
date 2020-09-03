namespace Northwind.Application.Employees.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using Common.Interfaces;
  using Domain.Entities;
  using FluentValidation;
  using MediatR;

  public class Create
  {
    public class Command : IRequest<int>
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
      public byte[]? Photo { get; set; }
      public int? ManagerId { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(v => v.FirstName).MaximumLength(50).NotEmpty();
        RuleFor(v => v.LastName).MaximumLength(50).NotEmpty();
        RuleFor(v => v.BirthDate).NotEmpty();
        RuleFor(v => v.HireDate).NotEmpty();
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
        var entity = new Employee
        {
          TitleOfCourtesy = command.Title,
          FirstName = command.FirstName,
          LastName = command.LastName,
          BirthDate = command.BirthDate,
          Address = command.Address,
          City = command.City,
          Region = command.Region,
          PostalCode = command.PostalCode,
          Country = command.Country,
          HomePhone = command.HomePhone,
          Title = command.Position,
          Extension = command.Extension,
          HireDate = command.HireDate,
          Notes = command.Notes,
          // Photo = command.Photo,
          ReportsTo = command.ManagerId
        };

        await _db.Employees.AddAsync(entity, token);
        await _db.SaveChangesAsync(token);

        return entity.EmployeeId;

      }
      
    }
    
  }
  
}