namespace Northwind.Application.Products.Commands
{
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  using AutoMapper;
  using AutoMapper.QueryableExtensions;
  using Common.Interfaces;
  using Common.Mappings;
  using Domain.Entities;
  using FluentValidation;
  using MediatR;
  using Microsoft.AspNetCore.Http;

  public class Import
  {
    public class Command : IRequest<IEnumerable<Model>>
    {
      public IFormFile File { get; set; }
    }

    public class Model
    {
      public int ProductId { get; set; }
      public string ProductName { get; set; }
      public int? SupplierId { get; set; }
      public int? CategoryId { get; set; }
      public string QuantityPerUnit { get; set; }
      public decimal? UnitPrice { get; set; }
      public short? UnitsInStock { get; set; }
      public short? UnitsOnOrder { get; set; }
      public short? ReorderLevel { get; set; }
      public bool Discontinued { get; set; }      
    }
    
    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(v => v.File.Length > 0);
      }
    }
    
    public class Handler : IRequestHandler<Command, IEnumerable<Model>>
    {
      private readonly INorthwindDbContext _db;
      private readonly IMapper _mapper;
      private readonly ICsvFileReader _fileReader;

      public Handler(INorthwindDbContext db, IMapper mapper, ICsvFileReader fileReader)
      {
        _db = db;
        _mapper = mapper;
        _fileReader = fileReader;
      }

      public async Task<IEnumerable<Model>> Handle(Command command, CancellationToken token)
      {
        var products = _fileReader.ReadProductsFile(command.File).ToList();

        foreach (var product in products)
        {
          var entity = new Product
          {
            ProductName = product.ProductName,
            SupplierId = product.SupplierId,
            CategoryId = product.CategoryId,
            QuantityPerUnit = product.QuantityPerUnit,
            UnitPrice = product.UnitPrice,
            UnitsInStock = product.UnitsInStock,
            UnitsOnOrder = product.UnitsOnOrder,
            ReorderLevel = product.ReorderLevel,
            Discontinued = product.Discontinued
          };

          await _db.Products.AddAsync(entity, token);
          await _db.SaveChangesAsync(token);

          product.ProductId = entity.ProductId;

        }

        return products;

      }
      
    }
    
  }
  
}