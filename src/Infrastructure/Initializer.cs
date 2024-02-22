using Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Initializer
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
    {
        return services.ConfigureSql();
    }
}