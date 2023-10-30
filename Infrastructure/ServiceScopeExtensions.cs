using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

/// <summary>
/// Extension methods for <see cref="IServiceScope"/>
/// </summary>
public static class ServiceScopeExtensions
{
    /// <summary>
    /// Ensures the database is created
    /// </summary>
    /// <param name="scope"><see cref="IServiceScope"/> to extend</param>
    /// <returns><see cref="IServiceScope"/> where database creation was ensured</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static async Task EnsureDatabaseCreatedAsync(this IServiceScope scope)
    {
        if (scope is null)
        {
            throw new ArgumentNullException(nameof(scope));
        }

        var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
        _ = await context.Database.EnsureCreatedAsync();
    }
}
