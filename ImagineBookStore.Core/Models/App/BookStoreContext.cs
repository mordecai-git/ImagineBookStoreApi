using Microsoft.EntityFrameworkCore;

namespace ImagineBookStore.Core.Models.App;

/// <summary>
/// Represents the DbContext for the BookStore application, providing access to the database entities.
/// </summary>
public partial class BookStoreContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BookStoreContext"/> class.
    /// </summary>
    public BookStoreContext()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BookStoreContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by the context.</param>
    public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the model and defines relationships between entities.
    /// </summary>
    /// <param name="builder">The model builder used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure default schema
        builder.HasDefaultSchema("dbo");

        // Configure UserRoles composite key
        builder.Entity<UserRole>(entity =>
        {
            entity.HasKey(t => new { t.RoleId, t.UserId });
        });
    }

    /// <summary>
    /// Gets or sets the DbSet representing roles in the database.
    /// </summary>
    public DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing users in the database.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing user roles in the database.
    /// </summary>
    public DbSet<UserRole> UserRoles { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing books in the database.
    /// </summary>
    public DbSet<Book> Books { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing carts in the database.
    /// </summary>
    public DbSet<Cart> Carts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing orders in the database.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing order items in the database.
    /// </summary>
    public DbSet<OrderItem> OrderItems { get; set; }
}