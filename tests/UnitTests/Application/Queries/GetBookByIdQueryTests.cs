using System.Diagnostics.CodeAnalysis;
using Application.Queries;
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
        _unitOfWork = Fixture.Freeze<IUnitOfWork>();
        _handler = new GetBookByIdQueryHandler(_unitOfWork);
    }

    [Fact]
    public async void Handle_RepositoryReturnsFail_ReturnFail()
    {
        //Arrange
        var query = Fixture.Create<GetBookByIdQuery>();
        
        _unitOfWork.BookRepository
            .GetBookAsync(Arg.Any<Guid>())
            .Returns(Result.Fail(Fixture.Create<Error>()));

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
        var query = Fixture.Create<GetBookByIdQuery>();
        var book = Fixture.Build<Book>()
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