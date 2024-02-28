using FluentValidation;

namespace Application.Features.CreteBookReview;

public class CreateBookReviewCommandValidator : AbstractValidator<CreateBookReviewCommand>
{
    public CreateBookReviewCommandValidator()
    {
        RuleFor(x => x.Review).NotNull();
        RuleFor(x => x.Review.Review)
            .NotEmpty()
            .When(x => x.Review is not null);
        RuleFor(x => x.Review.ReviewerName)
            .NotEmpty()
            .When(x => x.Review is not null);
    }
}