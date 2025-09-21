// UserCreateDto.cs
namespace TooliRent.Application.DTOs.User;

public class UserCreateDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // klartext vid skapande
    public string Role { get; set; } = "Member";
}
