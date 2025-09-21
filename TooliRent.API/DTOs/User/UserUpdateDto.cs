// UserUpdateDto.cs
namespace TooliRent.Application.DTOs.User;

public class UserUpdateDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; } // valfritt att uppdatera
    public bool? IsActive { get; set; }
    public string? Role { get; set; }
}
