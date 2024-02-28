using System.Diagnostics.CodeAnalysis;
using Application.DTO.Output;
using Application.Features.ListAllBookReviews;
using AutoFixture;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using FluentResults;
using NSubstitute;
using Xunit;

namespace UnitTests.Application.Queries;

[ExcludeFromCodeCoverage]
public class ListAllBooksQueryTests : TestsBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ListAllBooksQueryHandler _handler;

    public ListAllBooksQueryTests()
    {
        _unitOfWork = _fixture.Freeze<IUnitOfWork>();
        _handler = new ListAllBooksQueryHandler(_unitOfWork);
    }

    [Fact]
    public async void Handle_RepositoryFails_ReturnsFail()
    {
        //Arrange
        var query = _fixture.Create<ListAllBooksQuery>();
        _unitOfWork.BookRepository
            .GetAllBooksAsync()
            .Returns(Result.Fail(_fixture.Create<Error>()));
        
        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsFailed.Should().BeTrue();
        await _unitOfWork.BookRepository.Received(1).GetAllBooksAsync();
    }

    [Fact]
    public async void Handle_RepositoryOk_ReturnsOk()
    {
        //Arrange
        var query = _fixture.Create<ListAllBooksQuery>();
        var books = _fixture.CreateMany<Book>();
        _unitOfWork.BookRepository
            .GetAllBooksAsync()
            .Returns(Result.Ok(books));
        
        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        await _unitOfWork.BookRepository.Received(1).GetAllBooksAsync();
        result.Value.Should().BeEquivalentTo(books.Select(x => x.MapToDto()));
    }
}