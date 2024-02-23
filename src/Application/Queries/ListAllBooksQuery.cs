using Domain.Models;
using Domain.Repositories;
using FluentResults;
using MediatR;

namespace Application.Queries;

public class ListAllBooksQuery : IRequest<Result<IEnumerable<Book>>>;

public class ListAllBooksQueryHandler(IBookRepository bookRepository)
    : IRequestHandler<ListAllBooksQuery, Result<IEnumerable<Book>>>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<Result<IEnumerable<Book>>> Handle(ListAllBooksQuery request, CancellationToken cancellationToken)
    {
        var productsResult = await bookRepository.GetAllBooksAsync();

        return productsResult;
    }
}