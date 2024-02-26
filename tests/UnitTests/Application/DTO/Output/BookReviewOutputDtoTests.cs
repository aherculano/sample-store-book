using System.Diagnostics.CodeAnalysis;
using Application.DTO.Output;
using AutoFixture;
using Domain.Models;
using FluentAssertions;
using Xunit;

namespace UnitTests.Application.DTO.Output;

[ExcludeFromCodeCoverage]
public class BookReviewOutputDtoTests: TestsBase
{
    [Fact]
    public void MapToDto_NullEntity_ReturnsNull()
    {
        //Arrange
        BookReview domain = null;
        
        //Act
        var dto = domain.MapToDto();

        //Assert
        dto.Should().BeNull();
    }

    [Fact]
    public void MapToDto_ValidDomainEntity_ReturnsValidDto()
    {
        //Arrange
        var bookReview = Fixture.Create<BookReview>();

        //Act
        var dto = bookReview.MapToDto();

        //Assert
        dto.UniqueIdentifier.Should().Be(bookReview.UniqueIdentifier);
        dto.Review.Should().Be(bookReview.Review);
        dto.Reviewer.Should().Be(bookReview.Reviewer);
        dto.ReviewDate.Should().Be(bookReview.ReviewDate);
    }
}