﻿using System.Diagnostics.CodeAnalysis;
using Application.Commands;
using Application.DTO.Input;
using Application.DTO.Output;
using AutoFixture;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using FluentResults;
using NSubstitute;
using Xunit;

namespace UnitTests.Application.Command;

[ExcludeFromCodeCoverage]
public class CreateBookReviewCommandTests: TestsBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateBookReviewCommandHandler _handler;
    
    public CreateBookReviewCommandTests()
    {
        _unitOfWork = _fixture.Freeze<IUnitOfWork>();
        _handler = new CreateBookReviewCommandHandler(_unitOfWork);
    }

    [Fact]
    public async void Handle_GetBookReturnsFail_ReturnsFail()
    {
        //Arrange
        var command = _fixture.Create<CreateBookReviewCommand>();
        _unitOfWork.BookRepository
            .GetBookAsync(Arg.Any<Guid>())
            .Returns(Result.Fail(_fixture.Create<Error>()));

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsFailed.Should().BeTrue();
        await _unitOfWork.Received(0).BeginTransactionAsync();
        await _unitOfWork.Received(0).CommitAsync();
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Received(0).RollbackAsync();
        await _unitOfWork.BookRepository.Received(1).GetBookAsync(Arg.Any<Guid>());
    }
    
    [Fact]
    public async void Handle_CreateBookReviewReturnsFail_ReturnsFail()
    {
        //Arrange
        var command = _fixture.Create<CreateBookReviewCommand>();
        _unitOfWork.BookRepository
            .GetBookAsync(Arg.Any<Guid>())
            .Returns(_fixture.Create<Book>());
        
        _unitOfWork.BookRepository
            .CreateBookReviewAsync(Arg.Any<int>(), Arg.Any<BookReview>())
            .Returns(Result.Fail(_fixture.Create<Error>()));

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsFailed.Should().BeTrue();
        await _unitOfWork.Received(1).BeginTransactionAsync();
        await _unitOfWork.Received(0).CommitAsync();
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Received(1).RollbackAsync();
        await _unitOfWork.BookRepository.Received(1).GetBookAsync(Arg.Any<Guid>());
        await _unitOfWork.BookRepository.Received(1).CreateBookReviewAsync(Arg.Any<int>(), Arg.Any<BookReview>());
    }

    [Fact]
    public async void Handle_CreateBookReviewWorks_ReturnOk()
    {
        //Arrange
        var command = _fixture.Create<CreateBookReviewCommand>();
        var book = _fixture.Create<Book>();
        var review = _fixture
            .Build<BookReview>()
            .With(x => x.Review, command.Review.Review)
            .With(x => x.Reviewer, command.Review.ReviewerName)
            .Create();
        
        _unitOfWork.BookRepository
            .GetBookAsync(Arg.Any<Guid>())
            .Returns(book);
        
        _unitOfWork.BookRepository
            .CreateBookReviewAsync(book.Id, Arg.Any<BookReview>())
            .Returns(Result.Ok(review));

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(review.MapToDto());
        await _unitOfWork.Received(1).BeginTransactionAsync();
        await _unitOfWork.Received(1).CommitAsync();
        await _unitOfWork.Received(1).SaveChangesAsync();
        await _unitOfWork.Received(0).RollbackAsync();
        await _unitOfWork.BookRepository.Received(1).GetBookAsync(Arg.Any<Guid>());
        await _unitOfWork.BookRepository.Received(1).CreateBookReviewAsync(Arg.Any<int>(), Arg.Any<BookReview>());
    }

    [Fact]
    public void Validator_ValidCommand_ReturnsOk()
    {
        //Arrange
        var validator = new CreateBookReviewCommandValidator();
        var command = _fixture.Create<CreateBookReviewCommand>();
        
        //Act
        var validationResult = validator.Validate(command);

        //Assert
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void Validator_NullCommand_ReturnsInvalid()
    {
        //Arrange
        var validator = new CreateBookReviewCommandValidator();
        var command = _fixture
            .Build<CreateBookReviewCommand>()
            .With(x => x.Review, (BookReviewInputDto) null)
            .Create();
        
        //Act
        var validationResult = validator.Validate(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Count.Should().Be(1);
    }
    
    [Fact]
    public void Validator_InvalidCommand_ReturnsInvalid()
    {
        //Arrange
        var validator = new CreateBookReviewCommandValidator();
        var dto = _fixture.Build<BookReviewInputDto>()
            .With(x => x.Review, string.Empty)
            .With(x => x.ReviewerName, string.Empty)
            .Create();
        
        var command = _fixture
            .Build<CreateBookReviewCommand>()
            .With(x => x.Review, dto)
            .Create();
        
        //Act
        var validationResult = validator.Validate(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Count.Should().Be(2);
    }
}