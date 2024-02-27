﻿using Application.Commands;
using Application.DTO.Input;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("v1/books/{bookId}/reviews")]
public class BookReviewsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetReviewsAsync([FromRoute] Guid bookId)
    {
        var query = new ListAllBookReviewsQuery(bookId);
        var result = await mediator.Send(query);
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReviewAsync([FromRoute] Guid bookId, [FromBody] BookReviewInputDto review)
    {
        var command = new CreateBookReviewCommand(bookId, review);
        var result = await mediator.Send(command);
        return CreatedAtAction("GetReviewById",new { bookId, reviewId = result.Value.UniqueIdentifier }, result.Value);
    }

    [HttpGet("{reviewId}")]
    public async Task<IActionResult> GetReviewByIdAsync([FromRoute] Guid bookId, [FromRoute] Guid reviewId)
    {
        var query = new GetBookReviewByIdQuery(bookId, reviewId);
        var result = await mediator.Send(query);
        return Ok(result.Value);
    }
}