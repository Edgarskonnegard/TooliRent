namespace TooliRent.Application.DTOs.Statistics;
public class ToolUsageDto
{
    public int ToolId { get; set; }
    public string ToolName { get; set; } = string.Empty;
    public int TotalBookings { get; set; }           
    public double TotalDaysBooked { get; set; }      
    public bool IsAvailable { get; set; }            
}