using System.Text.Json;
using Application.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace API.ResultExtensions;

internal static class ResultExtensions
{
    public static ActionResult ToFailedActionResult<T>(this Result<T> result)
    {
        return HandleErrorResult(result);
    }

    private static ActionResult HandleErrorResult<T>(Result<T> result)
    {
        var errors = result.Errors;
        switch (errors)
        {
            case var _ when errors.Any(x => x is NotFoundError):
                return HandleNotFoundErrorResult(result);
            case var _ when errors.Any(x => x is ValidationError):
                return HandleValidationErrorResult(result);
            default:
                return new BadRequestResult();
        }
    }

    private static ActionResult HandleNotFoundErrorResult<T>(Result<T> result)
    {
        var problemDetails = new ProblemDetails()
        {
            Status = 404,
            Type = "Not Found",
            Title = "Resource Not Found",
            Detail = "The Requested Resource Was Not Found"
        };

        return new NotFoundObjectResult(problemDetails);
    }

    private static ActionResult HandleValidationErrorResult<T>(Result<T> result)
    {
        var validationError = result.Errors[0] as ValidationError;
        var failures = validationError.Failures;
        string details = string.Join("; ", failures.Select(failure => $"{failure.PropertyName}: {failure.ErrorMessage}"));
        var problemDetails = new ProblemDetails()
        {
            Status = 400,
            Title = "Bad Request",
            Type = "Bad Request",
            Detail = details
        };
        
        // var serializedDetails = JsonSerializer.Serialize(problemDetails);
        
        return new BadRequestObjectResult(problemDetails);
    }
}