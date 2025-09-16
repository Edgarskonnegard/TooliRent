using TooliRent.Application.Interfaces;
using TooliRent.Domain.Interfaces;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            await _repo.AddAsync(user, ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            await _repo.DeleteAsync(id, ct);
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken ct = default)
        {
            return await _repo.GetAllAsync(ct);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _repo.GetByEmailAsync(email, ct);
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _repo.GetByIdAsync(id, ct);
        }

        public async Task UpdateAsync(User user, CancellationToken ct = default)
        {
            await _repo.UpdateAsync(user, ct);
        }
    }
}
