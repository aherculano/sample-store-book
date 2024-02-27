using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Domain.Models;
using FluentAssertions;
using Infrastructure.Data.Models;
using Infrastructure.Data.Repositories.Mappers;
using Xunit;

namespace UnitTests.Infrastructure.Data.Repositories.Mappers;

[ExcludeFromCodeCoverage]
public class BookReviewMapper : TestsBase
{
    [Fact]
    public void MapToDomain_NullDbo_ReturnsNull()
    {
        //Arrange
        BookReviewDbo dbo = null;
        
        //Act
        var review = dbo.MapToDomain();
        
        //Assert
        review.Should().BeNull();
    }

    [Fact]
    public void MapToDomain_ValidDbo_ReturnsValidEntity()
    {
        //Arrange
        var dbo = _fixture.Create<BookReviewDbo>();
        
        //Act
        var domain = dbo.MapToDomain();
        
        //Assert
        domain.UniqueIdentifier.Should().Be(dbo.UniqueIdentifier);
        domain.Review.Should().Be(dbo.Review);
        domain.Reviewer.Should().Be(dbo.ReviewerName);
        domain.ReviewDate.Should().Be(dbo.ReviewDate);
    }

    [Fact]
    public void MapToDomain_NullList_ReturnsOk()
    {
        //Arrange
        IList<BookReviewDbo> dbos = null; 
        //Act
        var domain = dbos.MapToDomain();
        
        //Assert
        domain.Should().BeNull();
    }
    
    [Fact]
    public void MapToDomain_EmptyList_ReturnsOk()
    {
        //Arrange
        IList<BookReviewDbo> dbos = new List<BookReviewDbo>(); 
        //Act
        var domain = dbos.MapToDomain();
        
        //Assert
        domain.Should().BeEmpty();
    }
    
    [Fact]
    public void MapToDomain_ValidList_ReturnsOk()
    {
        //Arrange
        var dbos = _fixture.CreateMany<BookReviewDbo>().ToList();
        //Act
        var domain = dbos.MapToDomain();
        
        //Assert
        domain.Should().NotBeEmpty();
        domain.Count().Should().Be(dbos.Count);
    }

    [Fact]
    public void MapToDbo_NullEntity_ReturnsNull()
    {
        //Arrange
        BookReview book = null;
        
        //Act
        var dbo = book.MapToDbo(_fixture.Create<int>());
        
        //Assert
        dbo.Should().BeNull();
    }

    [Fact]
    public void MapToDbo_ValidEntity_ReturnsOk()
    {
        //Arrange
        var review = _fixture.Create<BookReview>();
        var bookId = _fixture.Create<int>();
        
        //Act
        var dbo = review.MapToDbo(bookId);
        
        //Assert
        dbo.Should().NotBeNull();
        dbo.UniqueIdentifier.Should().Be(review.UniqueIdentifier);
        dbo.BookId.Should().Be(bookId);
        dbo.Review.Should().Be(review.Review);
        dbo.ReviewerName.Should().Be(review.Reviewer);
    }
}