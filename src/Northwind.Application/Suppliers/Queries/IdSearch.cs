namespace Northwind.Application.Suppliers.Queries
{
  using System.Linq;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;
  using Common.Interfaces;
  using MediatR;
  using X.PagedList;

  public class IdSearch
  {
    public class Query : IRequest<string>
    {
      public string CompanyName { get; set; }
    }

    public class Handler : IRequestHandler<Query, string>
    {
      private readonly INorthwindDbContext _db;
    
      public Handler(INorthwindDbContext db) => _db = db;

      public async Task<string> Handle(Query query, CancellationToken token)
      {
        var items = await _db.Suppliers
          .Where(s => s.CompanyName.ToLower().Contains(query.CompanyName.ToLower()))
          .Select(x => new
          {
            key = x.CompanyName, 
            value = x.SupplierId.ToString()
          })
          .ToListAsync(token);

        var json = JsonSerializer.Serialize(items);

        return json;

      }
      
    }
    
  }
  
}