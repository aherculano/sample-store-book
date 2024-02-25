namespace Application.DTO.Input;

public record BookInputDto(
    string Title,
    string Author,
    string Genre,
    DateTimeOffset PublishDate);