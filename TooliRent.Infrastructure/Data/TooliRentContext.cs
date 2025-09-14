using Microsoft.EntityFrameworkCore;
using TooliRent.Domain.Models;

namespace TooliRent.Infrastructure.Data;

public class TooliRentContext : DbContext
{
    public TooliRentContext(DbContextOptions<TooliRentContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Tool> Tools => Set<Tool>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // User → Bookings (1-to-many)
    modelBuilder.Entity<User>()
        .HasMany(u => u.Bookings)
        .WithOne(b => b.User)
        .HasForeignKey(b => b.UserId);

    // Tool → Bookings (1-to-many)
    modelBuilder.Entity<Tool>()
        .HasMany(t => t.Bookings)
        .WithOne(b => b.Tool)
        .HasForeignKey(b => b.ToolId);

    // Category → Tools (1-to-many)
    modelBuilder.Entity<Category>()
        .HasMany(c => c.Tools)
        .WithOne(t => t.Category)
        .HasForeignKey(t => t.CategoryId);
}

}
