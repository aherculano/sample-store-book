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
public class GetBookReviewByIdQueryTests : TestsBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly GetBookReviewByIdQueryHandler _handler;
    
    public GetBookReviewByIdQueryTests()
    {
        _unitOfWork = _fixture.Freeze<IUnitOfWork>();
        _handler = new GetBookReviewByIdQueryHandler(_unitOfWork);
    }

    [Fact]
    public async void Handle_RepositoryThrows_ReturnsFail()
    {
        //Arrange
        var query = _fixture.Create<GetBookReviewByIdQuery>();
        _unitOfWork.BookRepository
            .GetBookReviewAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
            .Returns(Result.Fail(_fixture.Create<Error>()));
        
        //Act
        var result = await _handler.Handle(query, CancellationToken.None);
        
        //Assert
        result.IsFailed.Should().BeTrue();
        await _unitOfWork.BookRepository.Received(1).GetBookReviewAsync(query.BookUniqueIdentifier, query.ReviewUniqueIdentifier);
    }

    [Fact]
    public async void Handle_RepositoryOk_ReturnsOk()
    {
        //Arrange
        var query = _fixture.Create<GetBookReviewByIdQuery>();
        var book = _fixture.Build<BookReview>()
            .With(x => x.UniqueIdentifier, query.ReviewUniqueIdentifier)
            .Create();
        
        _unitOfWork.BookRepository
            .GetBookReviewAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
            .Returns(Result.Ok(book));
        
        //Act
        var result = await _handler.Handle(query, CancellationToken.None);
        
        //Assert
        result.IsSuccess.Should().BeTrue();
        await _unitOfWork.BookRepository.Received(1)
            .GetBookReviewAsync(query.BookUniqueIdentifier, query.ReviewUniqueIdentifier);
        result.Value.Should().Be(book.MapToDto());
    }
}