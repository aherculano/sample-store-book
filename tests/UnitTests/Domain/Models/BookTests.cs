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
        var bookUniqueIdentifier = Fixture.Create<Guid>();
        var bookTitle = Fixture.Create<string>();
        var bookAuthor = Fixture.Create<string>();
        var bookGenre = Fixture.Create<string>();
        var bookPublishDate = Fixture.Create<DateTimeOffset>();

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