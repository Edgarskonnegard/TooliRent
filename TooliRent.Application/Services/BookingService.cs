using AutoMapper;
using TooliRent.Application.DTOs.Booking;
using TooliRent.Application.Interfaces;
using TooliRent.Application.Mapping;
using TooliRent.Domain.Interfaces;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;

    public BookingService(IBookingRepository bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    public async Task<BookingReadDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var result = await _bookingRepository.GetByIdAsync(id, ct);
        return _mapper.Map<BookingReadDto>(result);
    }

    public async Task<IEnumerable<BookingReadDto>> GetAllAsync(CancellationToken ct = default)
    {
        var bookings = await _bookingRepository.GetAllAsync(ct);
        return _mapper.Map<IEnumerable<BookingReadDto>>(bookings);
    }

    public async Task<BookingReadDto?> AddAsync(BookingCreateDto bookingDto, CancellationToken ct = default)
    {
        var booking = _mapper.Map<Booking>(bookingDto);
        var createdBooking = await _bookingRepository.AddAsync(booking, ct);

        return _mapper.Map<BookingReadDto>(createdBooking);
    }

    public async Task<BookingReadDto?> UpdateAsync(int id, BookingUpdateDto updatedBookingDto, CancellationToken ct = default)
    {
        var booking = await _bookingRepository.GetByIdAsync(id, ct);

        if (booking == null)
        {
            throw new KeyNotFoundException($"Booking with id {id} not found");
        }

        var updatedBooking = _mapper.Map<Booking>(updatedBookingDto);

        booking.UserId = updatedBooking.UserId;
        booking.ToolId = updatedBooking.ToolId;
        booking.StartDate = updatedBooking.StartDate;
        booking.EndDate = updatedBooking.EndDate;
        booking.IsCollected = updatedBooking.IsCollected;
        booking.IsReturned = updatedBooking.IsReturned;

        var result = await _bookingRepository.UpdateAsync(booking, ct);
        return _mapper.Map<BookingReadDto>(result);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var bookingToDelete = await _bookingRepository.GetByIdAsync(id, ct);
        if (bookingToDelete == null) throw new KeyNotFoundException();

        await _bookingRepository.DeleteAsync(bookingToDelete, ct);
    }
}
