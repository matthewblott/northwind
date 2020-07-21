// namespace Northwind.WebUI.Tags
// {
//   using System.Threading.Tasks;
//   using Humanizer;
//   using Microsoft.AspNetCore.Mvc.ViewFeatures;
//   using Microsoft.AspNetCore.Razor.TagHelpers;
//
//   [HtmlTargetElement("label")]
//   public class DisplayNameTagHelper : TagHelper
//   {
//     [HtmlAttributeName("asp-for")]
//     public ModelExpression For { get; set; }
//     
//     public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
//     {
//       if (For == null)
//       {
//         return base.ProcessAsync(context, output);
//       }
//       
//       if (For.HasDisplayName())
//       {
//         return base.ProcessAsync(context, output);
//       }
//       
//       output.Content.SetContent(For.Name?.Titleize());
//       
//       return base.ProcessAsync(context, output);
//       
//     }
//     
//   }
//   
// }