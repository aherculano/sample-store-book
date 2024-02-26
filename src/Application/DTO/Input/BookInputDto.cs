using Domain.Models;

namespace Application.DTO.Input;

public record BookInputDto(
    string Title,
    string Author,
    string Genre,
    DateTimeOffset PublishDate);

public static class BookInputDtoMapper
{
    public static Book MapToDomain(this BookInputDto source)
    {
        if (source is null)
        {
            return null;
        }

        return new Book(source.Title, source.Author, source.Genre, source.PublishDate);
    }
}