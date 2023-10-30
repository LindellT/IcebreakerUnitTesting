using ApplicationServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

internal sealed class TodoContext : DbContext
{
    public TodoContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<TodoItem>().HasKey(x => x.Id);

        base.OnModelCreating(modelBuilder);
    }
}
