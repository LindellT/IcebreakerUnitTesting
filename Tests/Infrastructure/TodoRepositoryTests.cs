using ApplicationServices;
using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure;

public sealed class TodoRepositoryTests
{
    [Fact]
    public async Task GivenFindTodoItemIsCalled_WhenItemIsNotFound_ThenReturnsNull()
    {
        // Arrange
        using SqliteConnection connection = new("Filename=:memory:");
        await connection.OpenAsync();
        var contextOptions = new DbContextOptionsBuilder<TodoContext>()
            .UseSqlite(connection)
            .Options;
        using var context = new TodoContext(contextOptions);
        _ = await context.Database.EnsureCreatedAsync();

        var sut = new TodoRepository(context);

        // Act
        var result = await sut.GetByIdAsync(1, CancellationToken.None);

        // Assert
        _ = result.Should().BeNull();
    }

    [Fact]
    public async Task GivenFindTodoItemIsCalled_WhenItemIsFound_ThenReturnsCorrectly()
    {
        // Arrange
        using SqliteConnection connection = new("Filename=:memory:");
        await connection.OpenAsync();
        var contextOptions = new DbContextOptionsBuilder<TodoContext>()
            .UseSqlite(connection)
            .Options;
        using var context = new TodoContext(contextOptions);
        _ = await context.Database.EnsureCreatedAsync();
        var itemToAdd = new TodoItem { Description = "Eat in a Michelin Star restaurant", IsCompleted = true, };
        var entityEntry = await context.TodoItems.AddAsync(itemToAdd);
        _ = await context.SaveChangesAsync();
        var expected = new TodoItem { Id = entityEntry.Entity.Id, Description = itemToAdd.Description, IsCompleted = itemToAdd.IsCompleted, };

        var sut = new TodoRepository(context);

        // Act
        var result = await sut.GetByIdAsync(1, CancellationToken.None);

        // Assert
        _ = result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GivenDeleteTodoItemIsCalled_ThenDeletesItem()
    {
        // Arrange
        using SqliteConnection connection = new("Filename=:memory:");
        await connection.OpenAsync();
        var contextOptions = new DbContextOptionsBuilder<TodoContext>()
            .UseSqlite(connection)
            .Options;
        using var context = new TodoContext(contextOptions);
        _ = await context.Database.EnsureCreatedAsync();
        var itemToAdd = new TodoItem { Description = "Eat in a Michelin Star restaurant", IsCompleted = true, };
        var entityEntry = await context.TodoItems.AddAsync(itemToAdd);
        _ = await context.SaveChangesAsync();

        var sut = new TodoRepository(context);
        var entityToDelete = await sut.GetByIdAsync(entityEntry.Entity.Id, CancellationToken.None) ?? throw new ArgumentNullException();

        // Act
        await sut.DeleteAsync(entityToDelete, CancellationToken.None);
        var result = await sut.GetByIdAsync(entityEntry.Entity.Id, CancellationToken.None);

        // Assert
        _ = result.Should().BeNull();
    }
}
