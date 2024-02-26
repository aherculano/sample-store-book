using Domain.Models;

namespace Application.DTO.Output;

public record BookOutputDto(
    Guid UniqueIdentifier,
    string Title,
    string Author,
    string Genre,
    DateTimeOffset PublishDate);


public static class BookOutputDtoMapper
{
    public static BookOutputDto MapToDto(this Book source)
    {
        if (source is null)
        {
            return null;
        }

        return new BookOutputDto(source.UniqueIdentifier, source.Title, source.Author,source.Genre, source.PublishDate);
    }
}