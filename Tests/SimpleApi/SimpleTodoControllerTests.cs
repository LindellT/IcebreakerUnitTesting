using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Controllers;
using SimpleApi.Models;

namespace Tests.SimpleApi;

public sealed class SimpleTodoControllerTests
{
    [Fact]
    public async Task GivenDeleteSimpleTodoItemIsCalled_WhenSuccessful_ThenReturns204()
    {
        // Arrange
        using SqliteConnection connection = new("Filename=:memory:");
        await connection.OpenAsync();
        var contextOptions = new DbContextOptionsBuilder<SimpleTodoContext>()
            .UseSqlite(connection)
            .Options;
        using var context = new SimpleTodoContext(contextOptions);
        _ = await context.Database.EnsureCreatedAsync();
        var itemToAdd = new SimpleTodoItem { Description = "Eat in a Michelin Star restaurant", IsCompleted = true, };
        var entityEntry = await context.SimpleTodoItems.AddAsync(itemToAdd);
        _ = await context.SaveChangesAsync();
        var id = entityEntry.Entity.Id;

        var sut = new SimpleTodoController(context);

        // Act
        var result = await sut.DeleteSimpleTodoItemAsync(id, CancellationToken.None);

        // Assert
        _ = result.Should().NotBeNull().And.BeOfType<NoContentResult>().Which.StatusCode.Should().Be(204);
    }

    [Fact]
    public async Task GivenDeleteSimpleTodoItemIsCalled_WhenItemNotFound_ThenReturns404()
    {
        // Arrange
        using SqliteConnection connection = new("Filename=:memory:");
        await connection.OpenAsync();
        var contextOptions = new DbContextOptionsBuilder<SimpleTodoContext>()
            .UseSqlite(connection)
            .Options;
        using var context = new SimpleTodoContext(contextOptions);
        _ = await context.Database.EnsureCreatedAsync();
        var sut = new SimpleTodoController(context);

        // Act
        var result = await sut.DeleteSimpleTodoItemAsync(1, CancellationToken.None);

        // Assert
        _ = result.Should().NotBeNull().And.BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GivenDeleteSimpleTodoItemIsCalled_WhenSuccessful_ThenItemIsDeleted()
    {
        // Arrange
        using SqliteConnection connection = new("Filename=:memory:");
        await connection.OpenAsync();
        var contextOptions = new DbContextOptionsBuilder<SimpleTodoContext>()
            .UseSqlite(connection)
            .Options;
        using var context = new SimpleTodoContext(contextOptions);
        _ = await context.Database.EnsureCreatedAsync();
        var itemToAdd = new SimpleTodoItem { Description = "Eat in a Michelin Star restaurant", IsCompleted = true, };
        var entityEntry = await context.SimpleTodoItems.AddAsync(itemToAdd);
        _ = await context.SaveChangesAsync();
        var id = entityEntry.Entity.Id;

        var sut = new SimpleTodoController(context);

        // Act
        _ = await sut.DeleteSimpleTodoItemAsync(id, CancellationToken.None);
        var result = await context.SimpleTodoItems.FirstOrDefaultAsync(i => i.Id == id);

        // Assert
        _ = result.Should().BeNull();
    }
}
