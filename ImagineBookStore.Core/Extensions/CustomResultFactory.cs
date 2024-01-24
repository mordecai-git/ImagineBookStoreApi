using ImagineBookStore.Core.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace ImagineBookStore.Core.Extensions;

/// <summary>
/// Implements an <see cref="IFluentValidationAutoValidationResultFactory"/> to create custom action results for FluentValidation auto-validation.
/// </summary>
public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    /// <summary>
    /// Creates a custom <see cref="IActionResult"/> for FluentValidation auto-validation errors.
    /// </summary>
    /// <param name="context">The action executing context.</param>
    /// <param name="validationProblemDetails">The validation problem details containing validation errors.</param>
    /// <returns>A <see cref="BadRequestObjectResult"/> with a custom error response containing validation errors of type <see cref="ErrorResult"/>.</returns>
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails validationProblemDetails)
    {
        // Create a custom error response with validation errors
        var errorResponse = new ErrorResult("Validation Errors", "");
        errorResponse.ValidationErrors = validationProblemDetails?.Errors;

        return new BadRequestObjectResult(errorResponse);
    }
}
