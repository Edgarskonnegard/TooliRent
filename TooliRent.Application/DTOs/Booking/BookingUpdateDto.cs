namespace TooliRent.Application.DTOs.Booking;

public class BookingUpdateDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCollected { get; set; }
    public bool IsReturned { get; set; }
}
