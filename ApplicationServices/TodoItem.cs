namespace ApplicationServices;

/// <summary>
/// Model for Todo items
/// </summary>
public sealed class TodoItem
{
    /// <summary>
    /// Identifier for the todo item
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Description for the todo item
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// A flag indicating whether the item is completed
    /// </summary>
    public bool IsCompleted { get; set; }
}
