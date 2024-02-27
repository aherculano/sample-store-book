using Application.DTO.Input;
using Application.DTO.Output;
using Domain.Interfaces;
using Domain.Repositories;
using FluentResults;
using MediatR;

namespace Application.Queries;

public class GetBookReviewByIdQuery(Guid bookUniqueIdentifier, Guid reviewUniqueIdentifier)
    : IRequest<Result<BookReviewOutputDto>>
{
    public Guid BookUniqueIdentifier = bookUniqueIdentifier;
    public Guid ReviewUniqueIdentifier = reviewUniqueIdentifier;
}

public class GetBookReviewByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBookReviewByIdQuery, Result<BookReviewOutputDto>>
{
    private readonly IBookRepository _bookRepository = unitOfWork.BookRepository;
    
    public async Task<Result<BookReviewOutputDto>> Handle(GetBookReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var result =
            await _bookRepository.GetBookReviewAsync(request.BookUniqueIdentifier, request.ReviewUniqueIdentifier);
        
        if (result.IsSuccess)
        {
            return Result.Ok(result.Value.MapToDto());
        }

        return Result.Fail(new Error("Error getting book review"));
    }
}