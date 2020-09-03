namespace Northwind.WebUI.Validators
{
  using System;
  using System.Net;
  using System.Text;
  using Application.Common.Validators;
  using FluentValidation.AspNetCore;
  using FluentValidation.Internal;
  using FluentValidation.Validators;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
  using Microsoft.Extensions.DependencyInjection;

  public class RemoteClientValidator : ClientValidatorBase
  {
    private readonly RemoteValidator _validator;
    
    public RemoteClientValidator(PropertyRule rule, IPropertyValidator validator) : base(rule, validator)
    {
      _validator = validator as RemoteValidator ?? throw new NullReferenceException();
    }

    public override void AddValidation(ClientModelValidationContext context)
    {
      MergeAttribute(context.Attributes, "data-val", "true");

      var errorMessage = WebUtility.HtmlEncode($"'{Rule.GetDisplayName()}' is invalid.");
      
      MergeAttribute(context.Attributes, "data-val-remote", errorMessage);

      var additionalFields = _validator.AdditionalFields;
      var fields = additionalFields.Split(',', StringSplitOptions.RemoveEmptyEntries);
      var builder = new StringBuilder($"*.{context.ModelMetadata.PropertyName},");
      
      foreach (var field in fields)
      {
        builder.Append($"*.{field},");
      }
      
      var newFields = builder.ToString();
      
      if (newFields.EndsWith(","))
      {
        newFields = newFields.Substring(0, newFields.Length - 1);
      }
      
      MergeAttribute(context.Attributes, "data-val-remote-additionalfields", newFields);
      
      var action = _validator.Action;
      var controller = _validator.Controller;
      var urlHelper = context.ActionContext.HttpContext.RequestServices.GetService<IUrlHelper>();
      var url = urlHelper.ActionLink(action, controller);
      
      MergeAttribute(context.Attributes, "data-val-remote-url", url);

    }
    
  }
  
}