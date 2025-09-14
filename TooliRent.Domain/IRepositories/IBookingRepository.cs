using TooliRent.Domain.Models;

namespace TooliRent.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<Booking>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Booking booking, CancellationToken ct = default);
        Task UpdateAsync(Booking booking, CancellationToken ct = default);
        Task DeleteAsync(Booking booking, CancellationToken ct = default);
        Task<IEnumerable<Booking>> GetByUserIdAsync(int userId, CancellationToken ct = default);
    }
}
