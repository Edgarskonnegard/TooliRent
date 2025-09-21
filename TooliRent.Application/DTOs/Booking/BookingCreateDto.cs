namespace TooliRent.Application.DTOs.Booking;

public class BookingCreateDto
{
    public int UserId { get; set; }
    public int ToolId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
