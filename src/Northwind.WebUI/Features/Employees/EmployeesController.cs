namespace Northwind.WebUI.Features.Employees
{
  using Microsoft.AspNetCore.Mvc;
  using Application.Employees.Queries;
  using System.Threading.Tasks;
  using Application.Employees.Commands;
  using MediatR;
  
  public class EmployeesController : Controller
  {
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator) => _mediator = mediator;

    public async Task<IActionResult> Index(Index.Query query) => View(await _mediator.Send(query));
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