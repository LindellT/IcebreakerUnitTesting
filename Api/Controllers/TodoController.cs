using ApplicationServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Provides http endpoints to handle Todo items.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public sealed class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="todoService"><see cref="ITodoService"/></param>
    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    /// <summary>
    /// Delete a todo item.
    /// </summary>
    /// <param name="id">Id for the Todo item to be deleted</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTodoItemAsync(int id, CancellationToken cancellationToken)
    {
        return await _todoService.DeleteTodoItemAsync(id, cancellationToken) ? NoContent() : NotFound();
    }
}
