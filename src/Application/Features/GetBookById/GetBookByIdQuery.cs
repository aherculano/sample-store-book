using Application.DTO.Output;
using Application.Errors;
using Domain.Interfaces;
using Domain.Repositories;
using FluentResults;
using MediatR;

namespace Application.Features.GetBookById;

public class GetBookByIdQuery(Guid bookUniqueIdentifier) : IRequest<Result<BookOutputDto>>
{
    public readonly Guid BookUniqueIdentifier = bookUniqueIdentifier;
}

public class GetBookByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBookByIdQuery, Result<BookOutputDto>>
{
    private readonly IBookRepository _bookRepository = unitOfWork.BookRepository;
    
    public async Task<Result<BookOutputDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _bookRepository.GetBookAsync(request.BookUniqueIdentifier);

        if (result.IsSuccess)
        {
            return result.Value != null ? 
                Result.Ok(result.Value.MapToDto()) :
                Result.Fail(new NotFoundError($"Book {request.BookUniqueIdentifier} does not exist"));
        }

        return Result.Fail(new Error("Not Found"));
    }
}