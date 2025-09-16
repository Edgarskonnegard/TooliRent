using TooliRent.Application.Interfaces;
using TooliRent.Domain.Interfaces;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<Booking?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _bookingRepository.GetByIdAsync(id, ct);
    }

    public async Task<IEnumerable<Booking>> GetAllAsync(CancellationToken ct = default)
    {
        return await _bookingRepository.GetAllAsync(ct);
    }

    public async Task AddAsync(Booking booking, CancellationToken ct = default)
    {
        await _bookingRepository.AddAsync(booking, ct);
    }

    public async Task UpdateAsync(Booking booking, CancellationToken ct = default)
    {
        await _bookingRepository.UpdateAsync(booking, ct);
    }

    public async Task DeleteAsync(Booking booking, CancellationToken ct = default)
    {
        await _bookingRepository.DeleteAsync(booking, ct);
    }
}
