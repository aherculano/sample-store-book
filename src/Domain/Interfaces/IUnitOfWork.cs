using Domain.Repositories;

namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBookRepository BookRepository { get; }
    
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();

    Task SaveChangesAsync();
}