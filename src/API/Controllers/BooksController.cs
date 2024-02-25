using Application.Commands;
using Application.DTO.Input;
using Application.DTO.Output;
using Application.Queries;
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

    [HttpPost]
    public async Task<ActionResult<BookOutputDto>> CreateBook([FromBody] BookInputDto bookInputDto)
    {
        var command = new CreateBookCommand(bookInputDto);
        var result = await mediator.Send(command);
        if (result.IsFailed)
        {
            return BadRequest();
        }
        
        return Ok(result.Value);
    }
}