using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Infrastructure;

public sealed class ServiceScopeExtensionTests
{
    [Fact]
    public async Task GivenEnsureDatabaseCreatedAsyncIsCalled_WhenScopeIsNull_ThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        var scope = default(IServiceScope);

        var sut = async () => await scope!.EnsureDatabaseCreatedAsync();

        // Act & Assert
        _ = await sut.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GivenEnsureDatabaseCreatedAsyncIsCalled_WhenScopeDoesNotContainContext_ThenInvalidOperationExceptionIsThrown()
    {
        // Arrange
        var services = new ServiceCollection();
        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        var sut = scope.EnsureDatabaseCreatedAsync;

        // Act & Assert
        _ = await sut.Should().ThrowExactlyAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task GivenEnsureDatabaseCreatedAsyncIsCalled_WhenScopeIsSetupCorrectly_ThenDoesNotThrow()
    {
        // Arrange
        var services = new ServiceCollection();
        _ = services.AddInfrastructure(opt => opt.UseSqlite());
        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        var sut = scope.EnsureDatabaseCreatedAsync;

        // Act & Assert
        _ = await sut.Should().NotThrowAsync();
    }

    [Fact]
    public async Task GivenEnsureDatabaseCreatedAsyncIsCalled_WhenScopeIsSetupCorrectly_ThenCreatesDatabase()
    {
        // Arrange
        var services = new ServiceCollection();
        using SqliteConnection connection = new("Filename=:memory:");
        await connection.OpenAsync();
        _ = services.AddInfrastructure(opt => opt.UseSqlite(connection));
        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        await scope.EnsureDatabaseCreatedAsync();
        var context = scope.ServiceProvider.GetRequiredService<TodoContext>();

        var sut = async () => await context.TodoItems.CountAsync();

        // Act & Assert
        _ = await sut.Should().NotThrowAsync();
    }
}
