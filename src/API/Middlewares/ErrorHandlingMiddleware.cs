using System.Net.Mime;
using Domain.Interfaces;

namespace API.ErrorHandling;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    

    public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidOperationException)
        {
            await unitOfWork.RollbackAsync();

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("Internal Server Error");
        }
    }
}