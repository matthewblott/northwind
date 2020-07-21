// namespace Northwind.WebUI.Tags
// {
//   using Microsoft.AspNetCore.Http;
//   using Microsoft.AspNetCore.Razor.TagHelpers;
//   using System.Threading.Tasks;
//
//   [HtmlTargetElement(Attributes = "asp-authenticated")]
//   public class AuthenticatedTagHelper : TagHelper
//   {
//     private readonly IHttpContextAccessor _contextAccessor;
//
//     public AuthenticatedTagHelper(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;
//
//     public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
//     {
//       if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
//       {
//         output.SuppressOutput();
//       }
//
//       if (output.Attributes.TryGetAttribute("asp-authenticated", out var attribute))
//         output.Attributes.Remove(attribute);
//       
//       await Task.FromResult<object>(null);
//       
//     }
//     
//   }
//   
// }