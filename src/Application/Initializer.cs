using Application.Commands;
using Application.DTO.Output;
using Application.PipelineBehaviors;
using FluentResults;
using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

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