﻿using System.Diagnostics.CodeAnalysis;
using Application.Features.CreteBookReview;
using Application.DTO.Output;
using FluentResults;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.PipelineBehaviors;

[ExcludeFromCodeCoverage]
internal static class MediatrPipelineExtensions
{
    public static IServiceCollection ConfigurePipelineBehavior(this IServiceCollection services)
    {
        return services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
            .AddTransient<IPipelineBehavior<CreateBookCommand, Result<BookOutputDto>>, ValidationBehavior<CreateBookCommand, BookOutputDto>>()
            .AddTransient<IPipelineBehavior<CreateBookReviewCommand, Result<BookReviewOutputDto>>, ValidationBehavior<CreateBookReviewCommand, BookReviewOutputDto>>();
    }
}