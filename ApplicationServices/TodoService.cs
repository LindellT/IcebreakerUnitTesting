namespace ApplicationServices;

internal sealed class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<bool> DeleteTodoItemAsync(int id, CancellationToken cancellationToken)
    {
        var item = await _todoRepository.GetByIdAsync(id, cancellationToken);

        if (item is null)
        {
            return false;
        }

        await _todoRepository.DeleteAsync(item, cancellationToken);

        return true;
    }
}
