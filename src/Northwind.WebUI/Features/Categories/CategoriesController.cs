namespace Northwind.WebUI.Features.Categories
{
  using System.ComponentModel.DataAnnotations;
  using System.Globalization;
  using System.IO;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Application.Categories.Commands.DeleteCategory;
  using Application.Categories.Commands.UpsertCategory;
  using Application.Categories.Queries.GetCategoriesList;
  using System.Threading.Tasks;
  using CsvHelper;
  using MediatR;
  using Microsoft.AspNetCore.Hosting;

  public class CategoriesController : Controller
  {
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<CategoriesListVm>> GetAll() 
      => Ok(await _mediator.Send(new GetCategoriesListQuery()));

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

    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Import([FromServices] IWebHostEnvironment env, IFormFile file)
    {
      var path = Path.Combine(env.ContentRootPath, "Files", file.FileName);

      using (var reader = new StreamReader("path\\to\\file.csv"))
      using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
      {    
        csv.Configuration.PrepareHeaderForMatch = (header, index) => header.ToLower();
        
        // var records = csv.GetRecords<Foo>();
      }
      
      // Path.GetRandomFileName());
      
      await using (var stream = System.IO.File.Create(path))
        await file.CopyToAsync(stream);

      return RedirectToAction(nameof(Index));
      
    }
    
  }
  
}