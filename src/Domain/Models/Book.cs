using Domain.Common;

namespace Domain.Models;

public class Book(
    Guid uniqueIdentifier,
    string title,
    string author,
    string genre,
    DateTimeOffset publishDate) : Entity<int>
{
    public Guid UniqueIdentifier { get; set; } = uniqueIdentifier;

    public string Title { get; set; } = title;
    
    public string Author { get; set; } = author;
    
    public string Genre { get; set; } = genre;
    
    public DateTimeOffset PublishDate { get; set; } = publishDate;

}