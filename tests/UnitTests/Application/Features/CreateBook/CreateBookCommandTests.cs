using System.Diagnostics.CodeAnalysis;
using Application.Features.CreteBookReview;
using Application.DTO.Input;
using Application.DTO.Output;
using AutoFixture;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using FluentResults;
using NSubstitute;
using Xunit;

namespace UnitTests.Application.Features.CreateBook;

[ExcludeFromCodeCoverage]
public class CreateBookCommandTests : TestsBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateBookCommandHandler _handler;
    
    public CreateBookCommandTests()
    {
        _unitOfWork = _fixture.Freeze<IUnitOfWork>();
        _handler = new CreateBookCommandHandler(_unitOfWork);
    }

    [Fact]
    public async void Handle_RepositoryReturnsFailResult_ResultFail()
    {
        //Arrange
        var command = _fixture.Create<CreateBookCommand>();
        _unitOfWork.BookRepository.CreateBookAsync(Arg.Any<Book>()).Returns(Result.Fail(_fixture.Create<Error>()));
        
        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsFailed.Should().BeTrue();
        await _unitOfWork.Received(1).BeginTransactionAsync();
        await _unitOfWork.BookRepository.Received(1).CreateBookAsync(Arg.Any<Book>());
        await _unitOfWork.Received(1).RollbackAsync();
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Received(0).CommitAsync();
    }

    [Fact]
    public async void Handle_RepositoryReturnsOk_ResultOk()
    {
        //Arrange
        var command = _fixture.Create<CreateBookCommand>();
        var book = _fixture.Build<Book>()
            .With(x => x.Author, command.Book.Author)
            .With(x => x.Title, command.Book.Title)
            .With(x => x.Genre, command.Book.Genre)
            .With(x => x.PublishDate, command.Book.PublishDate)
            .Create();
        
        _unitOfWork.BookRepository.CreateBookAsync(Arg.Any<Book>()).Returns(Result.Ok(book));
        
        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(book.MapToDto());
        await _unitOfWork.Received(1).BeginTransactionAsync();
        await _unitOfWork.BookRepository.Received(1).CreateBookAsync(Arg.Any<Book>());
        await _unitOfWork.Received(0).RollbackAsync();
        await _unitOfWork.Received(1).SaveChangesAsync();
        await _unitOfWork.Received(1).CommitAsync();
    }
}