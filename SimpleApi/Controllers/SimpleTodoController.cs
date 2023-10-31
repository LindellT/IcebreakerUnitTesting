using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Models;

namespace SimpleApi.Controllers;

/// <summary>
/// Api endpoints for simple todo items.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public sealed class SimpleTodoController : ControllerBase
{
    private readonly SimpleTodoContext _simpleTodoContext;

    /// <summary>
    /// Constructor for setting up dependencies.
    /// </summary>
    /// <param name="simpleTodoContext">Database context</param>
    public SimpleTodoController(SimpleTodoContext simpleTodoContext)
    {
        _simpleTodoContext = simpleTodoContext;
    }

    /// <summary>
    /// Delete a simple todo item.
    /// </summary>
    /// <param name="id">Id for the simple todo item</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSimpleTodoItemAsync(int id, CancellationToken cancellationToken)
    {
        var simpleTodoItem = await _simpleTodoContext.SimpleTodoItems.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (simpleTodoItem is null)
        {
            return NotFound();
        }

        _ = _simpleTodoContext.SimpleTodoItems.Remove(simpleTodoItem);
        _ = await _simpleTodoContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
