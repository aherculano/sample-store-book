using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.EntityFramework;

public class BookContext : DbContext
{
    public DbSet<BookDbo> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
    }
}