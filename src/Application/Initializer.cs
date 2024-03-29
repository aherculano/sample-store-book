﻿using System.Diagnostics.CodeAnalysis;
using Application.Features.CreteBookReview;
using Application.DTO.Output;
using Application.PipelineBehaviors;
using FluentResults;
using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

[ExcludeFromCodeCoverage]
public static class Initializer
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        return services
            .ConfigureInfrastructure()
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Initializer).Assembly);
            })
            .ConfigurePipelineBehavior()
            .AddValidatorsFromAssembly(typeof(Initializer).Assembly);
    }
}