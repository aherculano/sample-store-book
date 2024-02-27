using System.Diagnostics.CodeAnalysis;
using Domain.Interfaces;
using Infrastructure.Data.EntityFramework;
using Infrastructure.Data.Repositories;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class SqlExtensions
{
    public static IServiceCollection ConfigureSql(this IServiceCollection services)
    {
        var settings = services.BuildServiceProvider().GetService<SqlSettings>();

        services.AddDbContext<BookContext>(
            options => options.UseSqlServer(settings.ConnectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}