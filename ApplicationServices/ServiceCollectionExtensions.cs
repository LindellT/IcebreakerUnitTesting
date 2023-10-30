using Microsoft.Extensions.DependencyInjection;

namespace ApplicationServices;

/// <summary>
/// Extension methods for IServiceCollection
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers application services to <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns>The <see cref="IServiceCollection"/> where application services were registered to</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        _ = services.AddTransient<ITodoService, TodoService>();
        return services;
    }
}
