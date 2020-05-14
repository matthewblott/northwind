namespace Northwind.WebUI.Controllers
{
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Application.Employees.Commands.DeleteEmployee;
  using Application.Employees.Commands.UpsertEmployee;
  using Application.Employees.Queries.GetEmployeeDetail;
  using Application.Employees.Queries.GetEmployeesList;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using MediatR;

  public class EmployeesController : Controller
  {
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator) => _mediator = mediator;

    public async Task<ActionResult<IList<EmployeeLookupDto>>> GetAll() 
      => Ok(await _mediator.Send(new GetEmployeesListQuery()));

    public async Task<ActionResult<EmployeeDetailVm>> Get(int id) 
      => Ok(await _mediator.Send(new GetEmployeeDetailQuery {Id = id}));
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Upsert(UpsertEmployeeCommand command)
    {
      var id = await _mediator.Send(command);

      return Ok(id);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
      await _mediator.Send(new DeleteEmployeeCommand {Id = id});

      return NoContent();
    }
  }
}