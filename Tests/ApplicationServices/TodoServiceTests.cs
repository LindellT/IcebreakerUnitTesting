using ApplicationServices;

namespace Tests.ApplicationServices;

public sealed class TodoServiceTests
{
    [Fact]
    public async Task GivenDeleteIsCalled_WhenSuccessful_ThenReturnsTrue()
    {
        // Arrange
        var sut = CreateSut();

        // Act
        var result = await sut.DeleteTodoItemAsync(1, CancellationToken.None);

        // Assert
        _ = result.Should().BeTrue();
    }

    [Fact]
    public async Task GivenDeleteIsCalled_WhenItemNotFound_ThenReturnsFalse()
    {
        // Arrange
        var todoRepository = Substitute.For<ITodoRepository>();
        _ = todoRepository.GetByIdAsync(default, default).ReturnsForAnyArgs(null as TodoItem);
        var sut = CreateSut(todoRepository);

        // Act
        var result = await sut.DeleteTodoItemAsync(2, CancellationToken.None);

        // Assert
        _ = result.Should().BeFalse();
    }

    [Fact]
    public async Task GivenDeleteIsCalled_WhenItemIsFound_ThenCallsDeleteForRepository()
    {
        // Arrange
        var todoRepository = Substitute.For<ITodoRepository>();
        _ = todoRepository.GetByIdAsync(default, default).ReturnsForAnyArgs(new TodoItem());
        var sut = CreateSut(todoRepository);

        // Act
        _ = await sut.DeleteTodoItemAsync(1, CancellationToken.None);

        // Assert
        await todoRepository.Received(1).DeleteAsync(Arg.Any<TodoItem>(), CancellationToken.None);
    }

    [Fact]
    public async Task GivenDeleteIsCalled_WhenRepositoryFindThrowsException_ThenDoesNotCatch()
    {
        // Arrange
        var todoRepository = Substitute.For<ITodoRepository>();
        _ = todoRepository.GetByIdAsync(default, default).ThrowsAsyncForAnyArgs<Exception>();
        var sut = CreateSut(todoRepository);

        // Act
        var result = async () => await sut.DeleteTodoItemAsync(1, CancellationToken.None);

        // Assert
        _ = await result.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task GivenDeleteIsCalled_WhenRepositoryDeleteThrowsException_ThenDoesNotCatch()
    {
        // Arrange
        var todoRepository = Substitute.For<ITodoRepository>();
        _ = todoRepository.GetByIdAsync(default, default).ReturnsForAnyArgs(new TodoItem());
        _ = todoRepository.DeleteAsync(default!, default).ThrowsAsyncForAnyArgs<Exception>();
        var sut = CreateSut(todoRepository);

        // Act
        var result = async () => await sut.DeleteTodoItemAsync(1, CancellationToken.None);

        // Assert
        _ = await result.Should().ThrowAsync<Exception>();
    }

    private static ITodoService CreateSut(ITodoRepository? todoRepository = null)
    {
        if (todoRepository is null)
        {
            todoRepository = Substitute.For<ITodoRepository>();
            _ = todoRepository.GetByIdAsync(default, default).ReturnsForAnyArgs(new TodoItem());
        }

        return new TodoService(todoRepository);
    }
}
