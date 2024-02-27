﻿using System.Diagnostics.CodeAnalysis;
using Application.Commands;
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
        _unitOfWork = Fixture.Freeze<IUnitOfWork>();
        _handler = new CreateBookReviewCommandHandler(_unitOfWork);
    }

    [Fact]
    public async void Handle_GetBookReturnsFail_ReturnsFail()
    {
        //Arrange
        var command = Fixture.Create<CreateBookReviewCommand>();
        _unitOfWork.BookRepository
            .GetBookAsync(Arg.Any<Guid>())
            .Returns(Result.Fail(Fixture.Create<Error>()));

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
        var command = Fixture.Create<CreateBookReviewCommand>();
        _unitOfWork.BookRepository
            .GetBookAsync(Arg.Any<Guid>())
            .Returns(Fixture.Create<Book>());
        
        _unitOfWork.BookRepository
            .CreateBookReviewAsync(Arg.Any<int>(), Arg.Any<BookReview>())
            .Returns(Result.Fail(Fixture.Create<Error>()));

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
        var command = Fixture.Create<CreateBookReviewCommand>();
        var book = Fixture.Create<Book>();
        var review = Fixture
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
}