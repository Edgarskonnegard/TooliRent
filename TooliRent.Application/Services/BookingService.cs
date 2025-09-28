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
    private readonly IToolRepository _toolRepository;
    private readonly IMapper _mapper;

    public BookingService(IBookingRepository bookingRepository, IToolRepository toolRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _toolRepository = toolRepository;
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
        var user = booking.User;
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        if (user.Role != "Member")
        {
            throw new InvalidOperationException("Only users with role member are allowed to be assigned as lenders.");
        }
        var tool = booking.Tool;
        if (tool == null)
        {
            throw new KeyNotFoundException("Tool not found.");
        }
        var totalDays = (booking.EndDate - booking.StartDate).Days + 1;
        booking.TotalPrice = totalDays * tool.PricePerDay;

        var createdBooking = await _bookingRepository.AddAsync(booking, ct);
        return _mapper.Map<BookingReadDto>(createdBooking);
    }

    public async Task<BookingReadDto?> CollectAsync(int id, CancellationToken ct = default)
    {
        var booking = await _bookingRepository.GetByIdAsync(id, ct);
        if (booking == null) throw new KeyNotFoundException($"Booking {id} not found");

        booking.IsCollected = true;
        booking.CollectedAt = DateTime.UtcNow;

        var result = await _bookingRepository.UpdateAsync(booking, ct);
        return _mapper.Map<BookingReadDto>(result);
    }

    public async Task<BookingReadDto?> ReturnAsync(int id, CancellationToken ct = default)
    {
        var booking = await _bookingRepository.GetByIdAsync(id, ct);
        if (booking == null) throw new KeyNotFoundException($"Booking {id} not found");

        booking.IsReturned = true;
        booking.ReturnedAt = DateTime.UtcNow;

        var tool = booking.Tool;
        if (tool != null)
        {
            var totalDays = (booking.EndDate - booking.StartDate).Days + 1;
            booking.TotalPrice = totalDays * tool.PricePerDay;
            if (booking.EndDate < DateTime.UtcNow)
            {
                booking.LateFee = (DateTime.UtcNow - booking.EndDate).Days * booking.Tool.PricePerDay * 0.1m;
            }
        }

        var result = await _bookingRepository.UpdateAsync(booking, ct);
        return _mapper.Map<BookingReadDto>(result);
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

    public async Task<IEnumerable<BookingReadDto?>> GetByUserIdAsync(int userId, CancellationToken ct)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId, ct);
        return _mapper.Map<IEnumerable<BookingReadDto?>>(bookings);
    }

    public async Task CancelAsync(int id, CancellationToken ct)
    {
        var booking = await _bookingRepository.GetByIdAsync(id, ct);
        if (booking == null)
            throw new KeyNotFoundException();

        // Bara avboka om den inte redan är avbokad, hämtad eller återlämnad
        if (!booking.IsCancelled && !booking.IsReturned && !booking.IsCollected)
        {
            booking.IsCancelled = true;
            await _bookingRepository.UpdateAsync(booking, ct);
        }

    }
    
    public async Task<IEnumerable<BookingReadDto>> GetOverdueAsync(CancellationToken ct = default)
    {
        var overdueBookings = await _bookingRepository.GetOverdueAsync(ct);

        return _mapper.Map<IEnumerable<BookingReadDto>>(overdueBookings);
    }

}
