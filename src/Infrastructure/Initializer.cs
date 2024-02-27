using System.Diagnostics.CodeAnalysis;
using Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

[ExcludeFromCodeCoverage]
public static class Initializer
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
    {
        return services.ConfigureSql();
    }
}