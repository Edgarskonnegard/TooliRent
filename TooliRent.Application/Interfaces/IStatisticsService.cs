using TooliRent.Application.DTOs.Statistics;

namespace TooliRent.Application.Interfaces;

public interface IStatisticsService
{
    // Verktygsstatistik: hur ofta verktygen är bokade vs lediga
    Task<List<ToolUsageDto>> GetToolsUsageAsync(CancellationToken ct = default);

    // Bokningsstatistik
    Task<BookingStatisticsDto> GetBookingStatisticsAsync(CancellationToken ct = default);

    // Användarstatistik
    Task<List<UserStatisticsDto>> GetUserStatisticsAsync(CancellationToken ct = default);

    // Intäktsstatistik
    Task<RevenueStatisticsDto> GetRevenueStatisticsAsync(CancellationToken ct = default);
}
