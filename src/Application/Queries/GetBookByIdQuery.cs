using Application.DTO.Output;
using Domain.Interfaces;
using Domain.Repositories;
using FluentResults;
using MediatR;

namespace Application.Queries;

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
            return Result.Ok(new BookOutputDto(
                result.Value.UniqueIdentifier,
                result.Value.Title,
                result.Value.Author,
                result.Value.Genre,
                result.Value.PublishDate));
        }

        return Result.Fail(new Error("Not Found"));
    }
}