using TooliRent.Domain.Models;

namespace TooliRent.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(User user, CancellationToken ct = default);
        Task UpdateAsync(User user, CancellationToken ct = default);
        Task DeleteAsync(User user, CancellationToken ct = default);
    }
}
