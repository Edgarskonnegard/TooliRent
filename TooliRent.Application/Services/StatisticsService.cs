using TooliRent.Application.DTOs.Statistics;
using TooliRent.Application.Interfaces;
using TooliRent.Domain.Interfaces;

namespace TooliRent.Application.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IToolRepository _toolRepository;
    private readonly IUserRepository _userRepository;

    public StatisticsService(
        IBookingRepository bookingRepository,
        IToolRepository toolRepository,
        IUserRepository userRepository)
    {
        _bookingRepository = bookingRepository;
        _toolRepository = toolRepository;
        _userRepository = userRepository;
    }

    // Verktygsstatistik: hur ofta verktyg är bokade vs lediga
    public async Task<List<ToolUsageDto>> GetToolsUsageAsync(CancellationToken ct = default)
    {
        var tools = await _toolRepository.GetAllAsync(ct);
        var bookings = await _bookingRepository.GetAllAsync(ct);

        var result = tools.Select(tool =>
        {
            var totalBookings = bookings.Count(b => b.ToolId == tool.Id && !b.IsCancelled);
            var totalDaysBooked = bookings
                .Where(b => b.ToolId == tool.Id && !b.IsCancelled)
                .Sum(b => (b.EndDate - b.StartDate).TotalDays);

            return new ToolUsageDto
            {
                ToolId = tool.Id,
                ToolName = tool.Name,
                TotalBookings = totalBookings,
                TotalDaysBooked = totalDaysBooked,
                IsAvailable = tool.IsAvailable
            };
        }).ToList();

        return result;
    }

    // Bokningsstatistik
    public async Task<BookingStatisticsDto> GetBookingStatisticsAsync(CancellationToken ct = default)
    {
        var bookings = await _bookingRepository.GetAllAsync(ct);

        var activeBookings = bookings.Count(b => !b.IsCancelled && !b.IsReturned);
        var cancelledBookings = bookings.Count(b => b.IsCancelled);
        var overdueBookings = bookings.Count(b => !b.IsReturned && !b.IsCancelled && b.EndDate < DateTime.UtcNow);
        var averageLength = bookings.Any() 
            ? bookings.Average(b => (b.EndDate - b.StartDate).TotalDays) 
            : 0;

        return new BookingStatisticsDto
        {
            ActiveBookings = activeBookings,
            CancelledBookings = cancelledBookings,
            OverdueBookings = overdueBookings,
            AverageBookingLength = averageLength
        };
    }

    // Användarstatistik
    public async Task<List<UserStatisticsDto>> GetUserStatisticsAsync(CancellationToken ct = default)
    {
        var users = await _userRepository.GetAllAsync(ct);
        var bookings = await _bookingRepository.GetAllAsync(ct);

        var result = users.Select(user =>
        {
            var userBookings = bookings.Where(b => b.UserId == user.Id);
            var totalLateFees = userBookings.Sum(b => b.LateFee);

            return new UserStatisticsDto
            {
                UserId = user.Id,
                Username = user.Username,
                TotalBookings = userBookings.Count(),
                OverdueBookings = userBookings.Count(b => !b.IsReturned && !b.IsCancelled && b.EndDate < DateTime.UtcNow),
                TotalLateFees = totalLateFees
            };
        }).ToList();

        return result;
    }

    // Intäktsstatistik
    public async Task<RevenueStatisticsDto> GetRevenueStatisticsAsync(CancellationToken ct = default)
    {
        var bookings = await _bookingRepository.GetAllAsync(ct);
        var tools = await _toolRepository.GetAllAsync(ct);

        var totalRevenue = bookings.Sum(b => b.TotalPrice);
        var totalLateFees = bookings.Sum(b => b.LateFee);

        var revenuePerTool = tools.Select(tool =>
        {
            var toolBookings = bookings.Where(b => b.ToolId == tool.Id);
            return new ToolRevenueDto
            {
                ToolId = tool.Id,
                ToolName = tool.Name,
                Revenue = toolBookings.Sum(b => b.TotalPrice),
                LateFees = toolBookings.Sum(b => b.LateFee)
            };
        }).ToList();

        return new RevenueStatisticsDto
        {
            TotalRevenue = totalRevenue,
            TotalLateFees = totalLateFees,
            RevenuePerTool = revenuePerTool
        };
    }
}
