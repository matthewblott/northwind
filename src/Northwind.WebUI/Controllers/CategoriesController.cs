namespace Northwind.WebUI.Controllers
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Application.Categories.Commands.DeleteCategory;
  using Application.Categories.Commands.UpsertCategory;
  using Application.Categories.Queries.GetCategoriesList;
  using System.Threading.Tasks;
  using MediatR;
  
  [Authorize]
  public class CategoriesController : Controller
  {
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<CategoriesListVm>> GetAll()
    {
      return Ok(await _mediator.Send(new GetCategoriesListQuery()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Upsert(UpsertCategoryCommand command)
    {
      var id = await _mediator.Send(command);

      return Ok(id);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
      await _mediator.Send(new DeleteCategoryCommand {Id = id});

      return NoContent();
    }
  }
}