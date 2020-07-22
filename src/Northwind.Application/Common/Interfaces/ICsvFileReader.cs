namespace Northwind.Application.Common.Interfaces
{
  using System.Collections.Generic;
  using Microsoft.AspNetCore.Http;
  using Products.Commands;

  public interface ICsvFileReader
  {
    IEnumerable<Import.Model> ReadProductsFile(IFormFile file);
    
    // byte[] BuildProductsFile(IEnumerable<ProductRecordDto> records);
  }
}