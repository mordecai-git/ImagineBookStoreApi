using ImagineBookStore.Core.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace ImagineBookStore.Core.Extensions;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails validationProblemDetails)
    {
        var errorResponse = new ErrorResult("Validation Errors", "");
        errorResponse.ValidationErrors = validationProblemDetails?.Errors;

        return new BadRequestObjectResult(errorResponse);
    }
}