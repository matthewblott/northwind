namespace Northwind.WebUI.Features.Customers
{
  using System;
  using Microsoft.AspNetCore.Mvc;
  using System.Threading.Tasks;
  using Application.Customers.Commands;
  using Application.Customers.Queries;
  using MediatR;
  using Shared;
  using Index = Application.Customers.Queries.Index;

  public class CustomersController : Controller
  {
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator) => _mediator = mediator;

    public IActionResult PostTest() => View(new PostTestViewModel());

    [HttpPost]
    public IActionResult PostTest(PostTestViewModel viewModel)
    {
      return View(new PostTestViewModel());
    }

    public async Task<IActionResult> IdAvailable(IdAvailable.Query query) => Json(await _mediator.Send(query));

    public async Task<IActionResult> Index(Index.Query query) => View(await _mediator.Send(query));

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      await _mediator.Send(command);

      return PartialView("_Message");
    }

    public async Task<IActionResult> Edit(Edit.Query query) => View(await _mediator.Send(query));

    [HttpPost]
    public async Task<IActionResult> Edit(Edit.Command command)
    {
      await _mediator.Send(command);

      var viewModel = new RedirectViewModel
      {
        Location =  $"Customer {command.Id} saved successfully."
      };

      var header = $"/Customers/Edit/{command.Id}";
      
      Response.Headers.Add("Turbolinks-Location", header);
      
      return PartialView("_Redirect", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Delete.Command command)
    {
      await _mediator.Send(command);

      return NoContent();
    }
    
  }
  
}