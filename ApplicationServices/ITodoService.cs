namespace ApplicationServices;

/// <summary>
/// Application service that handles coordination to modify todo items
/// </summary>
public interface ITodoService
{
    /// <summary>
    /// Deletes a todo item
    /// </summary>
    /// <param name="id">Id for the todo item</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>True when successful, false when todo item was not found</returns>
    Task<bool> DeleteTodoItemAsync(int id, CancellationToken cancellationToken);
}
