using Infrastructure;
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
            });
    }
}