using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Models;

[Table("Book")]
public class BookDbo
{
    public int Id { get; set; }
    
    public Guid UniqueIdentifier { get; set; }
    
    public string Title { get; set; }
    
    public string Author { get; set; }
    
    public string Genre { get; set; }
    
    public DateTimeOffset PublishDate { get; set; }
    
    public ICollection<BookReviewDbo> Reviews { get; set; }
}

public class BookReviewDbo
{
    public int Id { get; set; }
    
    public int BookId { get; set; }
    
    public string ReviewerName { get; set; }
    
    public string Review { get; set; }
    
    public BookDbo Book { get; set; }
}