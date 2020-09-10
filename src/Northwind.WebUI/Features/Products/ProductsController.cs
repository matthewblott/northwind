namespace Northwind.WebUI.Features.Products
{
  using System.Threading.Tasks;
  using Application.Products.Commands;
  using MediatR;
  using Microsoft.AspNetCore.Mvc;
  using Application.Products.Queries;
  using Index = Application.Products.Queries.Index;

  public class ProductsController : Controller
  {
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator) => _mediator = mediator;

    public async Task<IActionResult> Index(Index.Query query) => View( await _mediator.Send(query));

    [MyActionFilter]
    public async Task<IActionResult> Details(Details.Query query) 
      => View(await _mediator.Send(query));

    public IActionResult New() => View();
    
    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      await _mediator.Send(command);

      // Need to do something with the returned Id
      
      return RedirectToAction(nameof(Create));
    }
    
    [HttpPost]
    [MyActionFilter]
    public async Task<IActionResult> Update(Update.Command command)
    {
      await _mediator.Send(command);

      return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Delete.Command command)
    {
      await _mediator.Send(command);

      return NoContent();
    }

    public IActionResult Import() => View();

    [HttpPost]
    public async Task<IActionResult> Import(Import.Command command) 
      => View(await _mediator.Send(command));
    
  }
  
}