namespace TooliRent.Application.DTOs.Tool;

public class ToolCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
    public int CategoryId { get; set; }
}
