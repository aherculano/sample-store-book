﻿using System.Data;
using Application.DTO.Input;
using Application.DTO.Output;
using Domain.Interfaces;
using Domain.Models;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Features.CreteBookReview;

public class CreateBookReviewCommand(Guid bookUniqueIdentifier, BookReviewInputDto review) : IRequest<Result<BookReviewOutputDto>>
{
    public Guid BookUniqueIdentifier = bookUniqueIdentifier;

    public BookReviewInputDto Review = review;
}

public class CreateBookReviewCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBookReviewCommand, Result<BookReviewOutputDto>>
{
    public async Task<Result<BookReviewOutputDto>> Handle(CreateBookReviewCommand request, CancellationToken cancellationToken)
    {
        var bookResult = await unitOfWork.BookRepository.GetBookAsync(request.BookUniqueIdentifier);

        if (bookResult.IsFailed)
        {
            return Result.Fail("Error getting the book");
        }
        
        var book = bookResult.Value;

        await unitOfWork.BeginTransactionAsync();

        var review = new BookReview(request.Review.ReviewerName, request.Review.Review);

        var reviewResult = await unitOfWork.BookRepository.CreateBookReviewAsync(book.Id, review);

        if (reviewResult.IsFailed)
        {
            await unitOfWork.RollbackAsync();
            return Result.Fail(new Error("Error creating book review"));
        }
        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitAsync();
        
        return Result.Ok(reviewResult.Value.MapToDto());
    }
}