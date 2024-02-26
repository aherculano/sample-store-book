namespace Domain.Models;

public class BookReview
{
    public BookReview(Guid uniqueIdentifier, string reviewer, string review, DateTimeOffset reviewDate)
    {
        UniqueIdentifier = uniqueIdentifier;
        Reviewer = reviewer;
        Review = review;
        ReviewDate = reviewDate;
    }

    public BookReview(string reviewer, string review)
    {
        UniqueIdentifier = Guid.NewGuid();
        Reviewer = reviewer;
        Review = review;
        ReviewDate = DateTimeOffset.UtcNow;
    }

    public Guid UniqueIdentifier { get; set; }
    
    public string Reviewer { get; set; }
    
    public string Review { get; set; }
    
    public DateTimeOffset ReviewDate { get; set; }
}
