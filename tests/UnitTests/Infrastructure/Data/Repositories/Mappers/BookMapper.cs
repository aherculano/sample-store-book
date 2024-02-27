using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Domain.Models;
using FluentAssertions;
using Infrastructure.Data.Models;
using Infrastructure.Data.Repositories.Mappers;
using Xunit;

namespace UnitTests.Infrastructure.Data.Repositories.Mappers;

[ExcludeFromCodeCoverage]
public class BookMapper : TestsBase
{
    [Fact]
    public void MapToDomain_NullDbo_ReturnsNull()
    {
        //Arrange
        BookDbo dbo = null;
        
        //Act
        var book = dbo.MapToDomain();
        
        //Assert
        book.Should().BeNull();
    }

    [Fact]
    public void MapToDomain_ValidDbo_ReturnsValidEntity()
    {
        //Arrange
        var dbo = _fixture.Create<BookDbo>();
        
        //Act
        var domain = dbo.MapToDomain();
        
        //Assert
        domain.Id.Should().Be(dbo.Id);
        domain.UniqueIdentifier.Should().Be(dbo.UniqueIdentifier);
        domain.Author.Should().Be(dbo.Author);
        domain.Title.Should().Be(dbo.Title);
        domain.Genre.Should().Be(dbo.Genre);
        domain.PublishDate.Should().Be(dbo.PublishDate);
    }

    [Fact]
    public void MapToDomain_NullList_ReturnsOk()
    {
        //Arrange
        IList<BookDbo> dbos = null; 
        //Act
        var domain = dbos.MapToDomain();
        
        //Assert
        domain.Should().BeNull();
    }
    
    [Fact]
    public void MapToDomain_EmptyList_ReturnsOk()
    {
        //Arrange
        IList<BookDbo> dbos = new List<BookDbo>(); 
        //Act
        var domain = dbos.MapToDomain();
        
        //Assert
        domain.Should().BeEmpty();
    }
    
    [Fact]
    public void MapToDomain_ValidList_ReturnsOk()
    {
        //Arrange
        var dbos = _fixture.CreateMany<BookDbo>().ToList();
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
        Book book = null;
        
        //Act
        var dbo = book.MapToDbo();
        
        //Assert
        dbo.Should().BeNull();
    }

    [Fact]
    public void MapToDbo_ValidEntity_ReturnsOk()
    {
        //Arrange
        var book = _fixture.Create<Book>();
        
        //Act
        var dbo = book.MapToDbo();
        
        //Assert
        dbo.Should().NotBeNull();
        dbo.UniqueIdentifier.Should().Be(book.UniqueIdentifier);
        dbo.Title.Should().Be(book.Title);
        dbo.Author.Should().Be(book.Author);
        dbo.Genre.Should().Be(book.Genre);
        dbo.PublishDate.Should().Be(book.PublishDate);
    }
}