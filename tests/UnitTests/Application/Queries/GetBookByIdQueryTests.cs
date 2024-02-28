using System.Diagnostics.CodeAnalysis;
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
public class GetBookByIdQueryTests: TestsBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly GetBookByIdQueryHandler _handler;
    
    public GetBookByIdQueryTests()
    {
        _unitOfWork = _fixture.Freeze<IUnitOfWork>();
        _handler = new GetBookByIdQueryHandler(_unitOfWork);
    }

    [Fact]
    public async void Handle_RepositoryReturnsFail_ReturnFail()
    {
        //Arrange
        var query = _fixture.Create<GetBookByIdQuery>();
        
        _unitOfWork.BookRepository
            .GetBookAsync(Arg.Any<Guid>())
            .Returns(Result.Fail(_fixture.Create<Error>()));

        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsFailed.Should().BeTrue();
        await _unitOfWork.BookRepository.Received(1).GetBookAsync(query.BookUniqueIdentifier);
    }

    [Fact]
    public async void Handle_RepositoryReturnsOk_ResultOk()
    {
        //Arrange
        var query = _fixture.Create<GetBookByIdQuery>();
        var book = _fixture.Build<Book>()
            .With(x => x.UniqueIdentifier, query.BookUniqueIdentifier)
            .Create();
        _unitOfWork.BookRepository.GetBookAsync(query.BookUniqueIdentifier).Returns(Result.Ok(book));

        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        await _unitOfWork.BookRepository.Received(1).GetBookAsync(query.BookUniqueIdentifier);
    }
    
}