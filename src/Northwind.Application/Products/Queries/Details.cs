namespace Northwind.Application.Products.Queries
{
  using AutoMapper;
  using Domain.Entities;
  using System.Threading;
  using System.Threading.Tasks;
  using Common.Mappings;
  using Domain;
  using MediatR;

  public class Details
  {
    public class Query : IRequest<Model>
    {
      public int Id { get; set; }
    }
    
    public class Model : IMapFrom<Product>
    {
      public int Id { get; set; }
      public string ProductName { get; set; }
      public decimal? UnitPrice { get; set; }
      public int? SupplierId { get; set; }
      public string SupplierCompanyName { get; set; }
      public int? CategoryId { get; set; }
      public string CategoryName { get; set; }
      public bool Discontinued { get; set; }

      public void Mapping(Profile profile)
      {
        profile.CreateMap<Product, Model>()
          .ForMember(d => d.SupplierCompanyName,
            opt => opt.MapFrom(s => s.Supplier != null ? s.Supplier.CompanyName : string.Empty))
          .ForMember(d => d.CategoryName,
            opt => opt.MapFrom(s => s.Category != null ? s.Category.CategoryName : string.Empty));
        
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
        var entity = await _db.Products.FindAsync(query.Id);

        return _mapper.Map<Model>(entity);
      }
      
    }
    
  }
  
}