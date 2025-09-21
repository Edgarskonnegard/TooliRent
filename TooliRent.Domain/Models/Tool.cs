using System.ComponentModel.DataAnnotations;

namespace TooliRent.Domain.Models;

public class Tool
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }

    public bool IsAvailable { get; set; } = true;
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
    public List<Booking> Bookings { get; set; } = new();
}
