using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Domain.Models;
using FluentAssertions;
using Xunit;

namespace UnitTests.Domain.Models;

[ExcludeFromCodeCoverage]
public class BookTests : TestsBase
{
    [Fact]
    public void Ensure_BookConstructor_InstantiatesRightParameters()
    {
        //Arrange
        var bookUniqueIdentifier = _fixture.Create<Guid>();
        var bookTitle = _fixture.Create<string>();
        var bookAuthor = _fixture.Create<string>();
        var bookGenre = _fixture.Create<string>();
        var bookPublishDate = _fixture.Create<DateTimeOffset>();

        //Act
        var book = new Book(
            bookUniqueIdentifier,
            bookTitle,
            bookAuthor,
            bookGenre,
            bookPublishDate);
        
        //Assert
        book.UniqueIdentifier.Should().Be(bookUniqueIdentifier);
        book.Title.Should().Be(bookTitle);
        book.Author.Should().Be(bookAuthor);
        book.Genre.Should().Be(bookGenre);
        book.PublishDate.Should().Be(bookPublishDate);
        book.Id.Should().Be(default);
    }
}