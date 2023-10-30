using ApplicationServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers infrastructure to <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> to extend</param>
    /// <param name="dbContextOptionsBuilder">Action to configure the database</param>
    /// <returns>The <see cref="IServiceCollection"/> where infrastructure were registered to</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptionsBuilder)
    {
        _ = services.AddDbContext<TodoContext>(dbContextOptionsBuilder);
        _ = services.AddTransient<ITodoRepository, TodoRepository>();
        return services;
    }
}
