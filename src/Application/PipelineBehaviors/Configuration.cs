using Application.Commands;
using Application.DTO.Output;
using FluentResults;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.PipelineBehaviors;

internal static class Configuration
{
    public static IServiceCollection ConfigurePipelineBehavior(this IServiceCollection services)
    {
        return services
            .AddTransient<IPipelineBehavior<CreateBookCommand, Result<BookOutputDto>>, ValidationBehavior<CreateBookCommand, BookOutputDto>>()
            .AddTransient<IPipelineBehavior<CreateBookReviewCommand, Result<BookReviewOutputDto>>, ValidationBehavior<CreateBookReviewCommand, BookReviewOutputDto>>();
    }
}