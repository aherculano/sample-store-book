using FluentResults;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Errors;

public class ValidationError:Error
{
    public IEnumerable<ValidationFailure> Failures { get; }

    public ValidationError(IEnumerable<ValidationFailure> failures)
    {
        Failures = failures;
    }
}