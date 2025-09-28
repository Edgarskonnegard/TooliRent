using TooliRent.Domain.Models;

public interface IPasswordService
{
    string HashPassword(User user, string password);
    bool VerifyPassword(User user, string password);
}
