using Domain.Models;

namespace Application.DTO.Output;

public record BookReviewOutputDto(
    Guid UniqueIdentifier,
    string Reviewer,
    string Review,
    DateTimeOffset ReviewDate);

public static class BookReviewOutputDtoMapper
{
    public static BookReviewOutputDto MapToDto(this BookReview source)
    {
        if (source is null)
        {
            return null;
        }

        return new BookReviewOutputDto(source.UniqueIdentifier, source.Reviewer, source.Review, source.ReviewDate);
    }
}