using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using FluentResults;
using MediatR;

namespace Application.Queries;

public class ListAllBooksQuery : IRequest<Result<IEnumerable<Book>>>;

public class ListAllBooksQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListAllBooksQuery, Result<IEnumerable<Book>>>
{
    public async Task<Result<IEnumerable<Book>>> Handle(ListAllBooksQuery request, CancellationToken cancellationToken)
    {
        var productsResult = await unitOfWork.BookRepository.GetAllBooksAsync();

        return productsResult;
    }
}