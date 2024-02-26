using Domain.Interfaces;
using Domain.Models;
using FluentResults;

namespace Domain.Repositories;

public interface IBookRepository : IRepository
{
    Task<Result<IEnumerable<Book>>> GetAllBooksAsync();

    Task<Result<Book>> GetBookAsync(Guid uniqueIdentifier);

    Task<Result<Book>> CreateBookAsync(Book book);

    Task<Result<bool>> DeleteBookAsync(Book book);

    Task<Result<Book>> UpdateBookAsync(Book book);

    Task<Result<IEnumerable<BookReview>>> GetAllReviewsAsync(Guid bookUniqueIdentifier);

    Task<Result<BookReview>> GetBookReviewAsync(Guid bookUniqueIdentifier, Guid reviewUniqueIdentifier);

    Task<Result<BookReview>> CreateBookReviewAsync(int bookId, BookReview review);
}