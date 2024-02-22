using Domain.Models;
using Domain.Repositories;
using FluentResults;
using Infrastructure.Data.EntityFramework;
using Infrastructure.Data.Models;
using Infrastructure.Data.Repositories.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class BookRepository : IBookRepository
{
    private readonly DbSet<BookDbo> _dbSet;
    
    public BookRepository(DbSet<BookDbo> dbSet)
    {
        _dbSet = dbSet;
    }
    
    public async Task<Result<IList<Book>>> GetAllBooksAsync()
    {
        try
        {
            var bookList =  await _dbSet.ToListAsync();
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
            var dbo = await _dbSet.FirstOrDefaultAsync(book => book.UniqueIdentifier == uniqueIdentifier);

            return Result.Ok(dbo.MapToDomain());
        }
        catch (Exception e)
        {
            string errorMsg = $"Error getting the specified book ${uniqueIdentifier}";
            return Result.Fail(new Error(errorMsg).CausedBy(e));
        }
    }

    public async Task<Result<bool>> CreateBookAsync(Book book)
    {
        try
        {
            var result = await _dbSet.AddAsync(book.MapToDbo());

            return Result.Ok(result.State == EntityState.Added); 
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
}