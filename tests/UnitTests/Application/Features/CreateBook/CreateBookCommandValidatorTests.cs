using System.Diagnostics.CodeAnalysis;
using Application.DTO.Input;
using Application.Features.CreteBookReview;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace UnitTests.Application.Features.CreateBook;

[ExcludeFromCodeCoverage]
public class CreateBookCommandValidatorTests : TestsBase
{
    private readonly CreateBookCommandValidator _validator = new CreateBookCommandValidator();

    [Fact]
    public void Validator_ValidInputCommand_ReturnsOk()
    {
        //Arrange
        var command = _fixture.Create<CreateBookCommand>();
        
        //Act
        var validationResult = _validator.Validate(command);

        //Assert
        validationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validator_NullCommand_ReturnsInvalid()
    {
        //Arrange
        CreateBookCommand command = new CreateBookCommand(null);
        
        //Act
        var validationResult = _validator.Validate(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Count.Should().Be(1);
    }

    [Fact]
    public void Validator_InvalidInputCommand_ReturnsInvalid()
    {
        //Arrange
        var dto = _fixture.Build<BookInputDto>()
            .With(x => x.Author, string.Empty)
            .With(x => x.Title, string.Empty)
            .With(x => x.Genre, string.Empty)
            .Create();

        CreateBookCommand command = new CreateBookCommand(dto);
        
        //Act
        var validationResult = _validator.Validate(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Count.Should().Be(3);
    }
}