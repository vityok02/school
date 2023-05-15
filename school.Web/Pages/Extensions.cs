using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SchoolManagement.Web.Pages;

public static class Extensions
{
    public static void AddToModelState(
        this ValidationResult result,
        ModelStateDictionary modelState,
        string? propertyPrefix = null)
    {
        foreach (var error in result.Errors)
        {
            var name = string.IsNullOrWhiteSpace(propertyPrefix)
                ? error.PropertyName
                : $"{propertyPrefix}.{error.PropertyName}";
            modelState.AddModelError(name, error.ErrorMessage);
        }
    }
}
