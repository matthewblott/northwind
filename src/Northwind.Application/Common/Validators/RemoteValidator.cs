namespace Northwind.Application.Common.Validators
{
  using FluentValidation.Resources;
  using FluentValidation.Validators;

  // Create a validator, that does nothing on the server side.
  public class RemoteValidator : PropertyValidator
  {
    public string Action { get; }
    public string Controller { get; }
    public string AdditionalFields { get; }
    
    public RemoteValidator(string action, string controller, string additionalFields = "")
      : base(new LanguageStringSource(nameof(RemoteValidator)))
    {
      Action = action;
      Controller = controller;
      AdditionalFields = additionalFields;
    }

    // This is not a server side validation rule. So, should not effect at the server side.
    protected override bool IsValid(PropertyValidatorContext context) => true;
    
  }
  
}