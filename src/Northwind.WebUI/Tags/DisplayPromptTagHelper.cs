// namespace Northwind.WebUI.Tags
// {
//   using System.Threading.Tasks;
//   using Humanizer;
//   using Microsoft.AspNetCore.Mvc.ViewFeatures;
//   using Microsoft.AspNetCore.Razor.TagHelpers;
//   
//   [HtmlTargetElement("input", TagStructure = TagStructure.WithoutEndTag)]
//   public class DisplayPromptTagHelper : TagHelper
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
//       if (For.HasDisplayPrompt())
//       {
//         return base.ProcessAsync(context, output);
//       }
//
//       output.Attributes.Add(new TagHelperAttribute("placeholder", For.Name?.Titleize()));
//       
//       return base.ProcessAsync(context, output);
//       
//     }
//     
//   }
//   
// }