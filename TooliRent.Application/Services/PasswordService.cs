using Microsoft.AspNetCore.Identity;
using TooliRent.Domain.Models;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();

    public string HashPassword(User user, string password)
    {
        return _hasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string password)
    {
        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Success;
    }
}
