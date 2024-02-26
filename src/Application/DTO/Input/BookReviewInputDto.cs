using Domain.Models;

namespace Application.DTO.Input;

public record BookReviewInputDto(
    string ReviewerName,
    string Review);

internal static class BookReviewInputDtoMapper
{
    public static BookReview MapToDomain(this BookReviewInputDto source)
    {
        if (source is null)
        {
            return null;
        }

        return new BookReview(source.Review, source.ReviewerName);
    }
}