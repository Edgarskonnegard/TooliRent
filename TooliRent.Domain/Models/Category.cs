using System.ComponentModel.DataAnnotations;
namespace TooliRent.Domain.Models;

public class Category
{
    public int Id { get; set; }
    
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public List<Tool> Tools { get; set; } = new();
}
