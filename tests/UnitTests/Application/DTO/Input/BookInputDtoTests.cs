using System.Diagnostics.CodeAnalysis;
using Application.DTO.Input;
using AutoFixture;
using Domain.Models;
using FluentAssertions;
using Xunit;

namespace UnitTests.Application.DTO.Input;

[ExcludeFromCodeCoverage]
public class BookInputDtoTests : TestsBase
{
    [Fact]
    public void MapToDomain_NullInstance_ReturnsNull()
    {
        //Arrange
        BookInputDto dto = null;
        
        //Act
        var book = dto.MapToDomain();
        
        //Assert
        book.Should().BeNull();
    }

    [Fact]
    public void MapToDomain_ValidDto_ReturnsValidDomainObject()
    {
        //Arrange
        var dto = Fixture.Create<BookInputDto>();
        
        //Act
        var domain = dto.MapToDomain();
        
        //Assert
        domain.Id.Should().Be(default);
        domain.UniqueIdentifier.Should().NotBeEmpty();
        domain.Title.Should().Be(dto.Title);
        domain.Author.Should().Be(dto.Author);
        domain.Genre.Should().Be(dto.Genre);
        domain.PublishDate.Should().Be(dto.PublishDate);
    }
}
