namespace Northwind.Application.Employees.Queries
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using AutoMapper;
  using Domain.Entities;
  using System.Threading;
  using System.Threading.Tasks;
  using Common.Interfaces;
  using Common.Mappings;
  using FluentValidation;
  using MediatR;

  public class Details
  {
    public class Query : IRequest<Model>
    {
      public int Id { get; set; }
    }
    
    public class Validator : AbstractValidator<Query>
    {
      public Validator()
      {
      }
    }

    public class Model : IMapFrom<Employee>
    {
      public int Id { get; set; }
      public string Title { get; set; }
      public string FirstName { get; set; }
      [Required]
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

      public virtual IList<ModelTerritory> Territories { get; set; }
      
      public void Mapping(Profile profile)
      {
        profile.CreateMap<Employee, Model>()
          .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeId))
          .ForMember(d => d.Title, opt => opt.MapFrom(s => s.TitleOfCourtesy))
          .ForMember(d => d.Position, opt => opt.MapFrom(s => s.Title))
          .ForMember(d => d.ManagerId, opt => opt.MapFrom(s => s.ReportsTo))
          .ForMember(d => d.Territories, opts => opts.MapFrom(s => s.EmployeeTerritories));
      }
      
      public class ModelTerritory : IMapFrom<EmployeeTerritory>
      {
        public string TerritoryId { get; set; }
        public string Territory { get; set; }
        public string Region { get; set; }

        public void Mapping(Profile profile)
        {
          profile.CreateMap<EmployeeTerritory, ModelTerritory>()
            .ForMember(d => d.TerritoryId, opts => opts.MapFrom(s => s.TerritoryId))
            .ForMember(d => d.Territory, opts => opts.MapFrom(s => s.Territory.TerritoryDescription))
            .ForMember(d => d.Region, opts => opts.MapFrom(s => s.Territory.Region.RegionDescription));
        }
      }
      
    }
    
    public class Handler : IRequestHandler<Query, Model>
    {
      private readonly INorthwindDbContext _db;
      private readonly IMapper _mapper;

      public Handler(INorthwindDbContext db, IMapper mapper)
      {
        _db = db;
        _mapper = mapper;
      }

      public async Task<Model> Handle(Query query, CancellationToken token)
      {
        var id = query.Id;
        var entity = await _db.Employees.FindAsync(id);

        return _mapper.Map<Model>(entity);
      }
      
    }
    
  }
  
}