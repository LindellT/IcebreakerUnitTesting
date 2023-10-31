using Microsoft.EntityFrameworkCore;

namespace SimpleApi.Models;

/// <summary>
/// Database context for simple todo items.
/// </summary>
public sealed class SimpleTodoContext : DbContext
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options"><see cref="DbContextOptions"/> to configure the database</param>
    public SimpleTodoContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// Accessor for simple todo items.
    /// </summary>
    public DbSet<SimpleTodoItem> SimpleTodoItems { get; set; } = null!;

    /// <summary>
    /// Override to configure database mappings
    /// </summary>
    /// <param name="modelBuilder"><see cref="ModelBuilder"/></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder is null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        _ = modelBuilder.Entity<SimpleTodoItem>().HasKey(x => x.Id);

        base.OnModelCreating(modelBuilder);
    }
}
