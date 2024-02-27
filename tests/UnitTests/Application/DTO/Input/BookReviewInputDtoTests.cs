using System.Diagnostics.CodeAnalysis;
using Application.DTO.Input;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace UnitTests.Application.DTO.Input;

[ExcludeFromCodeCoverage]
public class BookReviewInputDtoTests : TestsBase
{
    [Fact]
    public void MapToDomain_NullEntity_ReturnsNull()
    {
        //Arrange
        BookReviewInputDto dto = null;
        
        //Act
        var domain = dto.MapToDomain();

        //Assert
        dto.Should().BeNull();
    }

    [Fact]
    public void MapToDomain_ValidDto_ReturnsValidDomainObject()
    {
        //Arrange
        var dto = _fixture.Create<BookReviewInputDto>();
        
        //Act
        var domain = dto.MapToDomain();

        //Assert
        domain.Review.Should().Be(dto.Review);
        domain.Reviewer.Should().Be(dto.ReviewerName);
        domain.ReviewDate.Should().NotBe(default);
        domain.UniqueIdentifier.Should().NotBeEmpty();
    }
}