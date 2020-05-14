namespace Northwind.WebUI.Controllers
{
  using System.Threading.Tasks;
  using MediatR;
  using Microsoft.AspNetCore.Mvc;
  using Application.Products.Commands.CreateProduct;
  using Application.Products.Commands.DeleteProduct;
  using Application.Products.Commands.UpdateProduct;
  using Application.Products.Queries;
  using Microsoft.AspNetCore.Http;

  public class ProductsController : Controller
  {
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator) => _mediator = mediator;

    public async Task<IActionResult> Index() => Ok(await _mediator.Send(new Index.Query()));
    
    public async Task<IActionResult> Details(Details.Query query) => Ok(await _mediator.Send(query));
    
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
    {
      var productId = await _mediator.Send(command);

      return Ok(productId);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
    {
      await _mediator.Send(command);

      return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
      await _mediator.Send(new DeleteProductCommand {Id = id});

      return NoContent();
    }

    public async Task<FileResult> Download()
    {
      var vm = await _mediator.Send(new File.Query());
    
      return File(vm.Content, vm.ContentType, vm.FileName);
    }
    
  }
  
}