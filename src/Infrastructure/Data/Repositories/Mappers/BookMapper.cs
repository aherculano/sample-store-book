using Domain.Models;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Repositories.Mappers;

public static class BookMapper
{
    public static IEnumerable<Book> MapToDomain(this IList<BookDbo> source)
    {
        if (source is null)
        {
            return null;
        }

        return source.Select(x => x.MapToDomain());
    }

    public static Book MapToDomain(this BookDbo source)
    {
        if (source is null)
        {
            return null;
        }

        return new Book(
            source.UniqueIdentifier,
            source.Title,
            source.Author,
            source.Genre,
            source.PublishDate)
        {
            Id = source.Id
        };
    }

    public static BookDbo MapToDbo(this Book source)
    {
        return new BookDbo()
        {
            UniqueIdentifier = source.UniqueIdentifier,
            Title = source.Title,
            Author = source.Author,
            Genre = source.Genre,
            PublishDate = source.PublishDate
        };
    }
}