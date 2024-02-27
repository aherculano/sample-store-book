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

    [HttpGet("{bookUniqueIdentifier}")]
    public async Task<ActionResult<BookOutputDto>> GetBookByIdAsync([FromRoute] Guid bookUniqueIdentifier)
    {
        var query = new GetBookByIdQuery(bookUniqueIdentifier);
        var result = await mediator.Send(query);
        if (result.IsFailed)
        {
            return BadRequest();
        }

        return result.Value != null ? Ok(result.Value) : NotFound();
    }
    
    [HttpPost]
    public async Task<ActionResult<BookOutputDto>> CreateBookAsync([FromBody] BookInputDto bookInputDto)
    {
        var command = new CreateBookCommand(bookInputDto);
        var result = await mediator.Send(command);
        if (result.IsFailed)
        {
            return BadRequest();
        }
        
        return CreatedAtAction("GetBookById", new { bookUniqueIdentifier = result.Value.UniqueIdentifier }, result.Value);
    }
}