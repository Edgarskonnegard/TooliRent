using TooliRent.Application.DTOs.Booking;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Interfaces;

public interface IBookingService
{
    Task<BookingReadDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<BookingReadDto>> GetAllAsync(CancellationToken ct = default);
    Task<BookingReadDto?> AddAsync(BookingCreateDto bookingDto, CancellationToken ct = default);
    Task<BookingReadDto?> UpdateAsync(int id, BookingUpdateDto updatedBookingDto, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
