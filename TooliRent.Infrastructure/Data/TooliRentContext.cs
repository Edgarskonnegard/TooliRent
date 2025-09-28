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

        modelBuilder.Entity<User>()
            .HasMany(u => u.Bookings)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<Tool>()
            .HasMany(t => t.Bookings)
            .WithOne(b => b.Tool)
            .HasForeignKey(b => b.ToolId);
        
        modelBuilder.Entity<Tool>()
            .Property(t => t.PricePerDay)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Tools)
            .WithOne(t => t.Category)
            .HasForeignKey(t => t.CategoryId);
        
         modelBuilder.Entity<Booking>(booking =>
        {
            booking.Property(b => b.TotalPrice).HasPrecision(18, 2);
            booking.Property(b => b.CollectedAt).IsRequired(false);
            booking.Property(b => b.ReturnedAt).IsRequired(false);
        });
        //admin password = Admin123?

}

}
