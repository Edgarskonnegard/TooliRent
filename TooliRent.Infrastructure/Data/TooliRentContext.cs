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
        // Här kan du lägga till constraints, seed-data och relationer
        base.OnModelCreating(modelBuilder);
    }
}
