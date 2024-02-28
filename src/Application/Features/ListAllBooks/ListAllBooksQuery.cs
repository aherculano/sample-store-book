using Application.DTO.Output;
using Domain.Interfaces;
using Domain.Models;
using FluentResults;
using MediatR;

namespace Application.Features.ListAllBookReviews;

public class ListAllBooksQuery : IRequest<Result<IEnumerable<BookOutputDto>>>;

public class ListAllBooksQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListAllBooksQuery, Result<IEnumerable<BookOutputDto>>>
{
    public async Task<Result<IEnumerable<BookOutputDto>>> Handle(ListAllBooksQuery request, CancellationToken cancellationToken)
    {
        var productsResult = await unitOfWork.BookRepository.GetAllBooksAsync();
        
        if (productsResult.IsSuccess)
        {
            return Result.Ok(productsResult.Value.Select(x => x.MapToDto()));
        }
        
        return Result.Fail("Error");
    }
}