namespace TooliRent.Application.DTOs.Tool;

public class ToolReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; }
    public int CategoryId { get; set; }
}
