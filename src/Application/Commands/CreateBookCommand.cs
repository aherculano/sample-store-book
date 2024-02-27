﻿using Application.DTO.Input;
using Application.DTO.Output;
using Domain.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Commands;

public class CreateBookCommand(BookInputDto book) : IRequest<Result<BookOutputDto>>
{
    public readonly BookInputDto Book = book;
}

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Book).NotNull();
        RuleFor(x => x.Book.Title)
            .NotEmpty()
            .When(x => x.Book is not null);
        RuleFor(x => x.Book.Author).NotNull()
            .NotEmpty()
            .When(x => x.Book is not null);;
        RuleFor(x => x.Book.Genre).NotNull()
            .NotEmpty()
            .When(x => x.Book is not null);;
        RuleFor(x => x.Book.PublishDate)
            .NotEmpty()
            .When(x => x.Book is not null);;
    }
}
public class CreateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBookCommand, Result<BookOutputDto>>
{
    public async Task<Result<BookOutputDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = request.Book.MapToDomain();
        await unitOfWork.BeginTransactionAsync();
        var result = await unitOfWork.BookRepository.CreateBookAsync(book);
        
        if (result.IsSuccess)
        {
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
            
            
            return Result.Ok(result.Value.MapToDto());
        }

        await unitOfWork.RollbackAsync();
        return Result.Fail(new Error("Error creating book"));
    }
}