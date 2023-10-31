using Api.Controllers;
using ApplicationServices;
using Microsoft.AspNetCore.Mvc;

namespace Tests.API;

public sealed class TodoControllerTests
{
    [Fact]
    public async Task GivenDeleteIsCalled_WhenSuccessful_ThenReturns204()
    {
        // Arrange
        var sut = CreateSut();

        // Act
        var result = await sut.DeleteTodoItemAsync(1, CancellationToken.None) as NoContentResult;

        // Assert
        _ = result.Should().NotBeNull()
            .And.BeOfType<NoContentResult>()
            .Which.StatusCode.Should().Be(204);
    }

    [Fact]
    public async Task GivenDeleteIsCalled_ThenCallsTodoService()
    {
        // Arrange
        var todoService = Substitute.For<ITodoService>();
        var sut = CreateSut(todoService);

        // Act
        _ = await sut.DeleteTodoItemAsync(1, CancellationToken.None);

        // Assert
        _ = await todoService.Received(1).DeleteTodoItemAsync(1, CancellationToken.None);
    }

    [Fact]
    public async Task GivenDeleteIsCalled_WhenItemNotFound_ThenReturns404()
    {
        // Arrange
        var todoService = Substitute.For<ITodoService>();
        _ = todoService.DeleteTodoItemAsync(default, default).ReturnsForAnyArgs(false);
        var sut = CreateSut(todoService);

        // Act
        var result = await sut.DeleteTodoItemAsync(1, CancellationToken.None);

        // Assert
        _ = result.Should().NotBeNull()
            .And.BeOfType<NotFoundResult>()
            .Which.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GivenDeleteIsCalled_WhenServiceThrowsException_ThenDoesNotCatch()
    {
        // Arrange
        var todoService = Substitute.For<ITodoService>();
        _ = todoService.DeleteTodoItemAsync(default, default).ThrowsAsyncForAnyArgs<Exception>();
        var sut = CreateSut(todoService);

        // Act
        var result = async () => await sut.DeleteTodoItemAsync(1, CancellationToken.None);

        // Assert
        _ = await result.Should().ThrowAsync<Exception>();
    }

    private static TodoController CreateSut(ITodoService? todoService = default)
    {
        if (todoService is null)
        {
            todoService = Substitute.For<ITodoService>();
            _ = todoService.DeleteTodoItemAsync(default, default).ReturnsForAnyArgs(true);
        }

        return new TodoController(todoService);
    }
}
