using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Data.EntityFramework;

namespace Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BookContext _bookContext;
    
    public IBookRepository BookRepository { get; private set; }
    public UnitOfWork(BookContext context)
    {
        _bookContext = context;
        BookRepository = new BookRepository(_bookContext.Books);
    }
    
    public void Dispose()
    {
        _bookContext.Dispose();
    }

    public async Task BeginTransactionAsync()
    {
        await _bookContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _bookContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackAsync()
    {
        await _bookContext.Database.CurrentTransaction.RollbackAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _bookContext.SaveChangesAsync();
    }
}