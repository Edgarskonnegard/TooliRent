using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TooliRent.Domain.Models;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int ToolId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public bool IsCollected { get; set; } = false;
    public bool IsReturned { get; set; } = false;
    public decimal TotalPrice { get; set; }

    public User? User { get; set; }

    public Tool? Tool { get; set; }
}
