using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityFramework;

public class BookConfiguration : IEntityTypeConfiguration<BookDbo>
{
    public void Configure(EntityTypeBuilder<BookDbo> builder)
    {
        builder.Property(book => book.UniqueIdentifier).IsRequired();
        builder.Property(book => book.Title).IsRequired();
        builder.Property(book => book.Author).IsRequired();
        builder.Property(book => book.Genre).IsRequired();
        builder.Property(book => book.PublishDate).IsRequired().HasPrecision(3);
        builder.HasKey(book => book.Id); 
        builder.ToTable("Book");
    }
}