using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.EntityFramework;

public class BookContext : DbContext
{
    public DbSet<BookDbo> Books { get; set; }

    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
    }
}