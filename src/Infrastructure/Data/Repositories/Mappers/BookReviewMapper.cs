using Domain.Models;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Repositories.Mappers;

public static class BookReviewMapper
{
    public static IEnumerable<BookReview> MapToDomain(this IEnumerable<BookReviewDbo> source)
    {
        if (source is null)
        {
            return null;
        }

        return source.Select(reviewDbo => MapToDomain(reviewDbo));
    }
    
    public static BookReview MapToDomain(this BookReviewDbo source)
    {
        if (source is null)
        {
            return null;
        }

        return new BookReview(
            source.UniqueIdentifier, 
            source.ReviewerName, 
            source.Review, 
            source.ReviewDate);
    }

    public static BookReviewDbo MapToDbo(this BookReview source, int bookId)
    {
        if (source is null)
        {
            return null;
        }
        
        return new BookReviewDbo()
        {
            ReviewerName = source.Reviewer,
            Review = source.Review,
            UniqueIdentifier = source.UniqueIdentifier,
            ReviewDate = source.ReviewDate,
            BookId = bookId
        };
    }
}