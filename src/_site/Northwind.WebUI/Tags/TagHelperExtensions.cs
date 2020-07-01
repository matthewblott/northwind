namespace Northwind.WebUI.Tags
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
  using Microsoft.AspNetCore.Mvc.ViewFeatures;

  public static class TagHelperExtensions
  {
    public static bool HasDisplayName(this ModelExpression expression)
      => !string.IsNullOrWhiteSpace(GetAttribute<DisplayAttribute>(expression)?.Name);
    
    public static bool HasDisplayPrompt(this ModelExpression expression) 
      => !string.IsNullOrWhiteSpace(GetAttribute<DisplayAttribute>(expression)?.Prompt);
    
    public static bool HasRequired(this ModelExpression expression)
      => GetAttribute<RequiredAttribute>(expression) != null;

    public static int GetStringLengthMin(this ModelExpression expression)
      => expression == null ? default : GetAttribute<StringLengthAttribute>(expression)?.MinimumLength ?? default;
    
    private static T GetAttribute<T>(ModelExpression expression)
    {
      if (expression == null)
      {
        return default;
      }

      var metadata = expression.ModelExplorer.Metadata as DefaultModelMetadata;
      var attributes = metadata?.Attributes.Attributes;
      var q = attributes?.FirstOrDefault(a => a.GetType() == typeof(T));
      var t = (T) q;

      return t;

    }
    
  }
  
}