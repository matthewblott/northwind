namespace Northwind.WebUI.Features.Suppliers
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Application.Suppliers.Commands;
  using Application.Suppliers.Queries;
  using MediatR;
  using Microsoft.AspNetCore.Mvc;
  using Index = Application.Suppliers.Queries.Index;

  public class SuppliersController : Controller
  {
    private readonly IMediator _mediator;

    public SuppliersController(IMediator mediator) => _mediator = mediator;

    public async Task<IActionResult> Index(Index.Query query) => View(await _mediator.Send(query));

    public async Task<IActionResult> Details(Details.Query query) => View(await _mediator.Send(query));
    
    public async Task<IActionResult> Create(Upsert.Query query) => View(await _mediator.Send(query));

    [HttpPost]
    public async Task<IActionResult> Create(Upsert.Command command)
    {
      await _mediator.Send(command);
      return NoContent();
    }

    public async Task<IActionResult> Edit(Upsert.Query query) => View(await _mediator.Send(query));

    [HttpPost]
    public async Task<IActionResult> Edit(Upsert.Command command)
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
    
    public async Task<IActionResult> IdSearch(IdSearch.Query query) 
      => Content(await _mediator.Send(query), "application/json");
    
  }
  
}