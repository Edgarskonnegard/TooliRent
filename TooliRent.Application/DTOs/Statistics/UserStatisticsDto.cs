namespace TooliRent.Application.DTOs.Statistics;

public class UserStatisticsDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public int TotalBookings { get; set; }          
    public int OverdueBookings { get; set; }        
    public decimal? TotalLateFees { get; set; }      
}