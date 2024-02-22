using Domain.Interfaces;
using Domain.Models;
using FluentResults;

namespace Domain.Repositories;

public interface IBookRepository : IRepository
{
    Task<Result<IEnumerable<Book>>> GetAllBooksAsync();

    Task<Result<Book>> GetBookAsync(Guid uniqueIdentifier);

    Task<Result<bool>> CreateBookAsync(Book book);

    Task<Result<bool>> DeleteBookAsync(Book book);

    Task<Result<Book>> UpdateBookAsync(Book book);
}