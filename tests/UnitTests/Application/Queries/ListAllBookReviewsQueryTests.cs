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
public class ListAllBookReviewsQueryTests : TestsBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ListAllBookReviewsQueryHandler _handler;

    public ListAllBookReviewsQueryTests()
    {
        _unitOfWork = _fixture.Freeze<IUnitOfWork>();
        _handler = new ListAllBookReviewsQueryHandler(_unitOfWork);
    }

    [Fact]
    public async void Handle_RepositoryFails_ReturnsFail()
    {
        //Arrange
        var query = _fixture.Create<ListAllBookReviewsQuery>();
        _unitOfWork.BookRepository
            .GetAllReviewsAsync(Arg.Any<Guid>())
            .Returns(Result.Fail(_fixture.Create<Error>()));
        
        //Act
        var result = await _handler.Handle(query, CancellationToken.None);
        
        //Assert   
        result.IsFailed.Should().BeTrue();
        await _unitOfWork.BookRepository.Received(1).GetAllReviewsAsync(query.BookUniqueIdentifier);
    }

    [Fact]
    public async void Handle_RepositoryOk_ReturnsOk()
    {
        //Arrange
        var query = _fixture.Create<ListAllBookReviewsQuery>();
        var reviews = _fixture.CreateMany<BookReview>();
        
        _unitOfWork.BookRepository
            .GetAllReviewsAsync(Arg.Any<Guid>())
            .Returns(Result.Ok(reviews));
        
        //Act
        var result = await _handler.Handle(query, CancellationToken.None);
        
        //Assert   
        result.IsSuccess.Should().BeTrue();
        await _unitOfWork.BookRepository
            .Received(1)
            .GetAllReviewsAsync(query.BookUniqueIdentifier);
        result.Value.Should().BeEquivalentTo(reviews.Select(x => x.MapToDto()));
    }
}