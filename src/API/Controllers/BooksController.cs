using Application.Queries;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("v1/books")]
[ApiController]
public class BooksController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var query = new ListAllBooksQuery();
        var result = await mediator.Send(query);
        return Ok(result.Value);
    }
}