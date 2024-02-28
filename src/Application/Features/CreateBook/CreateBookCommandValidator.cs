using FluentValidation;

namespace Application.Features.CreteBookReview;

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