using Microsoft.EntityFrameworkCore;

namespace ImagineBookStore.Core.Models.App;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext() { }

    public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("dbo");

        builder.Entity<UserRole>(entity =>
        {
            entity.HasKey(t => new { t.RoleId, t.UserId });
        });
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Cart> Carts { get; set; }
}