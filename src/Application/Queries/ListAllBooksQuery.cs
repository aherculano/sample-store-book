using Application.DTO.Output;
using Domain.Interfaces;
using Domain.Models;
using FluentResults;
using MediatR;

namespace Application.Queries;

public class ListAllBooksQuery : IRequest<Result<IEnumerable<BookOutputDto>>>;

public class ListAllBooksQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListAllBooksQuery, Result<IEnumerable<BookOutputDto>>>
{
    public async Task<Result<IEnumerable<BookOutputDto>>> Handle(ListAllBooksQuery request, CancellationToken cancellationToken)
    {
        var productsResult = await unitOfWork.BookRepository.GetAllBooksAsync();
        if (productsResult.IsSuccess)
        {
            return Result.Ok(productsResult.Value.Select(x =>
                new BookOutputDto(
                    x.UniqueIdentifier,
                    x.Title,
                    x.Author,
                    x.Genre,
                    x.PublishDate)));
        }
        
        return Result.Fail("Error");
    }
}