namespace Northwind.WebUI.Tags
{
  using System;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc.ViewFeatures;
  using Microsoft.AspNetCore.Razor.TagHelpers;

  [HtmlTargetElement("input", TagStructure = TagStructure.WithoutEndTag)]  
  public class StringLengthTagHelper : TagHelper
  {
    [HtmlAttributeName("asp-for")]
    public ModelExpression For { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
      if (For == null)
      {
        return base.ProcessAsync(context, output);
      }

      // var i = For.GetStringLengthMax();
      //
      // if (i > 0)
      // {
      //   output.Attributes.Add(new TagHelperAttribute("maxlength", i));
      // }

      var j = For.GetStringLengthMin();

      if (j > 0)
      {
        output.Attributes.Add(new TagHelperAttribute("minlength", j));
      }
      
      return base.ProcessAsync(context, output);

    }
    
  }
  
}