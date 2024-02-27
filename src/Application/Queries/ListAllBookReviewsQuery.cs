using Application.DTO.Output;
using Domain.Interfaces;
using Domain.Repositories;
using FluentResults;
using MediatR;

namespace Application.Queries;

public class ListAllBookReviewsQuery(Guid bookUniqueIdentifier) : IRequest<Result<IEnumerable<BookReviewOutputDto>>>
{
    public Guid BookUniqueIdentifier = bookUniqueIdentifier;
}

public class ListAllBookReviewsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<ListAllBookReviewsQuery, Result<IEnumerable<BookReviewOutputDto>>>
{
    private readonly IBookRepository _bookRepository = unitOfWork.BookRepository;
    
    public async Task<Result<IEnumerable<BookReviewOutputDto>>> Handle(ListAllBookReviewsQuery request, CancellationToken cancellationToken)
    {
        var result = await _bookRepository.GetAllReviewsAsync(request.BookUniqueIdentifier);

        if (result.IsSuccess)
        {
            return Result.Ok(result.Value.Select(x => x.MapToDto()));
        }

        return Result.Fail("Error getting all reviews");
    }
}