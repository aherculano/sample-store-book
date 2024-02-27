using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.PipelineBehaviors;

public class ValidationBehavior<TRequest, T> : IPipelineBehavior<TRequest, Result<T>>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<Result<T>> Handle(TRequest request, RequestHandlerDelegate<Result<T>> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        
        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .ToList();

        if (failures.Any())
        {
            var exception = new ValidationException(failures);
            return Result.Fail(new Error("Error validating command").CausedBy(exception));
        }

        return await next();
    }
}

// public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
// where TRequest: IRequest<TResponse>
// {
//     private readonly IEnumerable<IValidator<TRequest>> _validators;
//
//     public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
//     {
//         _validators = validators;
//     }
//
//     public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//     {
//         var context = new ValidationContext<TRequest>(request);
//         
//         var failures = _validators
//             .Select(v => v.Validate(context))
//             .SelectMany(result => result.Errors)
//             .Where(failure => failure != null)
//             .ToList();
//
//         if (failures.Any())
//         {
//             var exception = new ValidationException(failures);
//             throw exception;
//         }
//
//         return await next();
//     }
// }
