using TooliRent.Domain.Models;

namespace TooliRent.Application.Interfaces;

public interface IBookingService
{
    Task<Booking?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Booking>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Booking booking, CancellationToken ct = default);
    Task UpdateAsync(Booking booking, CancellationToken ct = default);
    Task DeleteAsync(Booking booking, CancellationToken ct = default);
}
