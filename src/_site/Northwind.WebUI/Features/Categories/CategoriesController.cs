namespace Northwind.WebUI.Features.Categories
{
  using Microsoft.AspNetCore.Mvc;
  using System.Threading.Tasks;
  using Application.Categories.Commands;
  using Application.Categories.Queries;
  using MediatR;
  
  public class CategoriesController : Controller
  {
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator) => _mediator = mediator;

    public async Task<IActionResult> Index() => View(await _mediator.Send(new Index.Query()));
    
    public async Task<IActionResult> Details(Details.Query query) => View(await _mediator.Send(query));

    public IActionResult New() => View();
    
    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      await _mediator.Send(command);

      return NoContent();
    }
    
    [HttpPost]
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
    
  }
  
}