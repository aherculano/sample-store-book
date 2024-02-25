using Application.DTO.Input;
using Application.DTO.Output;
using Domain.Interfaces;
using Domain.Models;
using FluentResults;
using MediatR;

namespace Application.Commands;

public class CreateBookCommand(BookInputDto book) : IRequest<Result<BookOutputDto>>
{
    public readonly BookInputDto Book = book;
}

public class CreateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBookCommand, Result<BookOutputDto>>
{
    public async Task<Result<BookOutputDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book(
            request.Book.Title,
            request.Book.Author,
            request.Book.Genre,
            request.Book.PublishDate);
        await unitOfWork.BeginTransactionAsync();
        var result = await unitOfWork.BookRepository.CreateBookAsync(book);
        
        if (result.IsSuccess)
        {
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
            
            var outputDto = new BookOutputDto(
                book.UniqueIdentifier,
                book.Title,
                book.Author,
                book.Genre,
                book.PublishDate);
            
            return Result.Ok(outputDto);
        }

        await unitOfWork.RollbackAsync();
        return Result.Fail(new Error("Error creating book"));
    }
}