using Domain.Common;

namespace Domain.Models;

public class Book : Entity<int>
{
    public Guid UniqueIdentifier { get; set; }

    public string Title { get; set; }
    
    public string Author { get; set; } 
    
    public string Genre { get; set; } 
    
    public DateTimeOffset PublishDate { get; set; }

    public Book(
        Guid uniqueIdentifier,
        string title,
        string author,
        string genre,
        DateTimeOffset publishDate)
    {
        UniqueIdentifier = uniqueIdentifier;
        Title = title;
        Author = author;
        Genre = genre;
        PublishDate = publishDate;
    }

    public Book(
        string title,
        string author,
        string genre,
        DateTimeOffset publishDate)
    {
        UniqueIdentifier = Guid.NewGuid();
        Title = title;
        Author = author;
        Genre = genre;
        PublishDate = publishDate;
    }
}