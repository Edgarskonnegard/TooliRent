using Microsoft.EntityFrameworkCore;
using TooliRent.Domain.Interfaces;
using TooliRent.Domain.Models;
using TooliRent.Infrastructure.Data;

namespace TooliRent.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly TooliRentContext _context;

        public BookingRepository(TooliRentContext context)
        {
            _context = context;
        }

        public async Task<Booking?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Bookings
                    .Include(b => b.Tool)
                    .Include(b => b.User)
                    .FirstOrDefaultAsync(b => b.Id == id, ct);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Bookings
                    .Include(b => b.Tool)
                    .Include(b => b.User)
                    .ToListAsync(ct);
        }

        public async Task<IEnumerable<Booking>> GetByUserIdAsync(int userId, CancellationToken ct = default)
        {
            return await _context.Bookings
                    .Where(b => b.UserId == userId)
                    .Include(b => b.Tool)
                    .Include(b => b.User)
                    .ToListAsync(ct);
        }

        public async Task AddAsync(Booking booking, CancellationToken ct = default)
        {
            await _context.Bookings.AddAsync(booking, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Booking booking, CancellationToken ct = default)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Booking booking, CancellationToken ct = default)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync(ct);
        }
    }
}
