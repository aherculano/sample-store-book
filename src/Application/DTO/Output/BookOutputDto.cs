namespace Application.DTO.Output;

public record BookOutputDto(
    Guid UniqueIdentifier,
    string Title,
    string Author,
    string Genre,
    DateTimeOffset PublishDate);