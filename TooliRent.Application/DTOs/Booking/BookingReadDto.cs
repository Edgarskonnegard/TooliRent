namespace TooliRent.Application.DTOs.Booking;

public class BookingReadDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ToolId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCollected { get; set; }
    public bool IsReturned { get; set; }
}
