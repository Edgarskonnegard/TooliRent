namespace TooliRent.Application.DTOs.Statistics;

public class ToolRevenueDto
{
    public int ToolId { get; set; }
    public string ToolName { get; set; } = string.Empty;
    public decimal Revenue { get; set; }       
    public decimal? LateFees { get; set; }      
}