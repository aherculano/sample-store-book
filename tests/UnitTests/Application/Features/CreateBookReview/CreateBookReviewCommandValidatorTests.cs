using System.Diagnostics.CodeAnalysis;
using Application.DTO.Input;
using Application.Features.CreteBookReview;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace UnitTests.Application.Features.CreateBookReview;

[ExcludeFromCodeCoverage]
public class CreateBookReviewCommandValidatorTests: TestsBase
{
    private readonly CreateBookReviewCommandValidator _validator = new CreateBookReviewCommandValidator();

    [Fact]
    public void Validator_ValidCommand_ReturnsOk()
    {
        //Arrange
        var command = _fixture.Create<CreateBookReviewCommand>();
        
        //Act
        var validationResult = _validator.Validate(command);

        //Assert
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void Validator_NullCommand_ReturnsInvalid()
    {
        //Arrange
        var command = _fixture
            .Build<CreateBookReviewCommand>()
            .With(x => x.Review, (BookReviewInputDto) null)
            .Create();
        
        //Act
        var validationResult = _validator.Validate(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Count.Should().Be(1);
    }
    
    [Fact]
    public void Validator_InvalidCommand_ReturnsInvalid()
    {
        //Arrange
        var dto = _fixture.Build<BookReviewInputDto>()
            .With(x => x.Review, string.Empty)
            .With(x => x.ReviewerName, string.Empty)
            .Create();
        
        var command = _fixture
            .Build<CreateBookReviewCommand>()
            .With(x => x.Review, dto)
            .Create();
        
        //Act
        var validationResult = _validator.Validate(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Count.Should().Be(2);
    }
}