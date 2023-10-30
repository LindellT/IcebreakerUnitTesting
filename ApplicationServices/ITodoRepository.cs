namespace ApplicationServices;

/// <summary>
/// Repository to handle todo items persistance
/// </summary>
public interface ITodoRepository
{
    /// <summary>
    /// Delete a todo item
    /// </summary>
    /// <param name="todoItem">Todo item to delete</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteAsync(TodoItem todoItem, CancellationToken cancellationToken);

    /// <summary>
    /// Get todo item by id
    /// </summary>
    /// <param name="id">Id for todo item</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>A <see cref="Task"/> containing the todo Item or null if not found</returns>
    Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken);
}
