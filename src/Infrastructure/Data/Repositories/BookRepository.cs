using Domain.Models;
using Domain.Repositories;
using FluentResults;
using Infrastructure.Data.Models;
using Infrastructure.Data.Repositories.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class BookRepository : IBookRepository
{
    private readonly DbSet<BookDbo> _bookBookDbSet;
    private readonly DbSet<BookReviewDbo> _reviewDbSet;
    
    public BookRepository(DbSet<BookDbo> bookDbSet, 
        DbSet<BookReviewDbo> reviewDbSet)
    {
        _bookBookDbSet = bookDbSet;
        _reviewDbSet = reviewDbSet;
    }
    
    public async Task<Result<IEnumerable<Book>>> GetAllBooksAsync()
    {
        try
        {
            var bookList =  await _bookBookDbSet.ToListAsync();
            return Result.Ok(bookList.MapToDomain());
        }
        catch (Exception e)
        {
            const string errorMsg = "Error listing all books in the database";
            return Result.Fail(new Error(errorMsg).CausedBy(e));
        }
    }

    public async Task<Result<Book>> GetBookAsync(Guid uniqueIdentifier)
    {
        try
        {
            var dbo = await _bookBookDbSet.FirstOrDefaultAsync(book => book.UniqueIdentifier == uniqueIdentifier);

            return Result.Ok(dbo.MapToDomain());
        }
        catch (Exception e)
        {
            string errorMsg = $"Error getting the specified book ${uniqueIdentifier}";
            return Result.Fail(new Error(errorMsg).CausedBy(e));
        }
    }

    public async Task<Result<Book>> CreateBookAsync(Book book)
    {
        try
        {
            var result = await _bookBookDbSet.AddAsync(book.MapToDbo());

            if (result.State == EntityState.Added)
            {
                return Result.Ok(result.Entity.MapToDomain());
            };

            return Result.Fail(new Error("Book cannot be created!"));
        }
        catch (Exception e)
        {
            const string errorMsg = "Error creating a book in the database";
            return Result.Fail(new Error(errorMsg).CausedBy(e));
        }
    }

    public async Task<Result<bool>> DeleteBookAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Book>> UpdateBookAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<BookReview>>> GetAllReviewsAsync(Guid bookUniqueIdentifier)
    {
        try
        {
            var reviews = await _reviewDbSet
                .Where(x => x.Book.UniqueIdentifier == bookUniqueIdentifier)
                .ToListAsync();
            
            return Result.Ok(reviews.MapToDomain());
        }
        catch (Exception e)
        {
            string errorMsg = $"Error listing all reviews in the database ${bookUniqueIdentifier}";
            return Result.Fail(new Error(errorMsg).CausedBy(e));
        }
    }

    public async Task<Result<BookReview>> GetBookReviewAsync(Guid bookUniqueIdentifier, Guid reviewUniqueIdentifier)
    {
        try
        {
            var review = await _reviewDbSet.Where(x => x.Book.UniqueIdentifier == bookUniqueIdentifier &&
                                                       x.UniqueIdentifier == reviewUniqueIdentifier)
                .FirstOrDefaultAsync();

            return Result.Ok(review.MapToDomain());
        }
        catch (Exception e)
        {
            string errorMsg = $"Error listing review in the database ${bookUniqueIdentifier} ${reviewUniqueIdentifier}";
            return Result.Fail(new Error(errorMsg).CausedBy(e));
        }
    }

    public async Task<Result<BookReview>> CreateBookReviewAsync(int bookId, BookReview review)
    {
        try
        {
            var result = await _reviewDbSet.AddAsync(review.MapToDbo(bookId));

            return Result.Ok(result.Entity.MapToDomain()); 
        }
        catch (Exception e)
        {
            const string errorMsg = "Error creating a review in the database";
            return Result.Fail(new Error(errorMsg).CausedBy(e));
        }
    }
}