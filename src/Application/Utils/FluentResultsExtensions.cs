using FluentResults;

namespace Application.Utils;

internal static class FluentResultsExtensions
{
    public static IResultBase ThrowExceptionIfHasFailedResult(this IResultBase source)
    {
        if (source.IsSuccess)
        {
            return source;
        }

        var errors = source.Errors.Select(x => x.Reasons.ToString()).ToString();

        throw new InvalidOperationException(errors);
    }
    
    public static IResult<TValue> ThrowExceptionIfHasFailedResult<TValue>(this IResult<TValue> source)
    {
        if (source.IsSuccess)
        {
            return source;
        }

        var errors = source.Errors.Select(x => x.Reasons.ToString()).ToString();

        throw new InvalidOperationException(errors);
    }
}