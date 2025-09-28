namespace TooliRent.Application.DTOs.Statistics;
public class BookingStatisticsDto
{
    public int ActiveBookings { get; set; }          
    public int CancelledBookings { get; set; }       
    public int OverdueBookings { get; set; }         
    public double AverageBookingLength { get; set; } 
}