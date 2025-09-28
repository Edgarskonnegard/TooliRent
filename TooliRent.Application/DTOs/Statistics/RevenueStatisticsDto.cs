namespace TooliRent.Application.DTOs.Statistics;
public class RevenueStatisticsDto
{
    public decimal TotalRevenue { get; set; }              // Totala intäkter
    public decimal? TotalLateFees { get; set; }             // Totala förseningsavgifter
    public List<ToolRevenueDto> RevenuePerTool { get; set; } = new();
}