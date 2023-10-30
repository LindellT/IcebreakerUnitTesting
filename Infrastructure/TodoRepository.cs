using ApplicationServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

internal sealed class TodoRepository : ITodoRepository
{
    private readonly TodoContext _context;

    public TodoRepository(TodoContext context)
    {
        _context = context;
    }

    public async Task DeleteAsync(TodoItem todoItem, CancellationToken cancellationToken)
    {
        _ = _context.TodoItems.Remove(todoItem);
        _ = await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
