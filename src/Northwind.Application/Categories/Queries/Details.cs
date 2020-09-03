namespace Northwind.Application.Categories.Queries
{
  using System.Collections.Generic;
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
        RuleFor(q => q.Id).NotEmpty();
      }
    }

    public class Model : IMapFrom<Category>
    {
      public int Id { get; set; }
      public string CategoryName { get; set; }
      public string Description { get; set; }
      
      public virtual IList<ModelProduct> Products { get; set; }
      
      public void Mapping(Profile profile)
      {
        profile.CreateMap<Category, Model>()
          .ForMember(d => d.Id,opt
            => opt.MapFrom(s => s.CategoryId))
          .ForMember(d => d.CategoryName, opt
            => opt.MapFrom(s => s.CategoryName))
          .ForMember(d => d.Description,opt
            => opt.MapFrom(s => s.Description));
      }
      
      public class ModelProduct : IMapFrom<Product>
      {
        public int Id { get; set; }
        public string ProductName { get; set; }
        
        public void Mapping(Profile profile)
        {
          profile.CreateMap<Product, ModelProduct>()
            .ForMember(d => d.Id, opts => opts.MapFrom(s => s.ProductId))
            .ForMember(d => d.ProductName, opts => opts.MapFrom(s => s.ProductName));;
          
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
        var entity = await _db.Categories.FindAsync(id);

        return _mapper.Map<Model>(entity);
      }
      
    }
    
  }
  
}