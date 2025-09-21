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

    public async Task UpdateAsync(int id, Booking updatedBooking, CancellationToken ct = default)
    {
        var booking = await _bookingRepository.GetByIdAsync(id, ct);

        if (booking == null)
        {
            throw new KeyNotFoundException($"Booking with id {id} not found");
        }

        booking.UserId = updatedBooking.UserId;
        booking.ToolId = updatedBooking.ToolId;
        booking.StartDate = updatedBooking.StartDate;
        booking.EndDate = updatedBooking.EndDate;
        booking.IsCollected = updatedBooking.IsCollected;
        booking.IsReturned = updatedBooking.IsReturned;

        await _bookingRepository.UpdateAsync(booking, ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var bookingToDelete = await _bookingRepository.GetByIdAsync(id, ct);
        if (bookingToDelete == null) throw new KeyNotFoundException();

        await _bookingRepository.DeleteAsync(bookingToDelete, ct);
    }
}
