using System.Diagnostics.CodeAnalysis;
using Application.DTO.Output;
using AutoFixture;
using Domain.Models;
using FluentAssertions;
using Xunit;

namespace UnitTests.Application.DTO.Output;

[ExcludeFromCodeCoverage]
public class BookOutputDtoTests : TestsBase
{
    [Fact]
    public void MapToDto_NullInstance_ReturnsNull()
    {
        //Arrange
        Book book = null;

        //Act
        var dto = book.MapToDto();

        //Assert
        dto.Should().BeNull();
    }

    [Fact]
    public void MapToDto_ValidInstance_ReturnsValidDto()
    {
        //Arrange
        Book book = Fixture.Create<Book>();
        
        //Act
        var dto = book.MapToDto();
        
        //Assert
        dto.UniqueIdentifier.Should().Be(book.UniqueIdentifier);
        dto.PublishDate.Should().Be(book.PublishDate);
        dto.Title.Should().Be(book.Title);
        dto.Author.Should().Be(book.Author);
        dto.Genre.Should().Be(book.Genre);
    }
}