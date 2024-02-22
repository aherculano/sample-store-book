using Infrastructure.Data.EntityFramework;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Migrations;

public class MigrationsDbContext : DbContext
{
    public MigrationsDbContext(DbContextOptions<MigrationsDbContext> options) : base(options)
    {
    }

    public DbSet<BookDbo> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
    }
    
}