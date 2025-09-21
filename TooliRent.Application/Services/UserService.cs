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
            var user = await _repo.GetByIdAsync(id);
            if (user != null)
            {
                await _repo.DeleteAsync(user, ct);
            }
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

        public async Task UpdateAsync(int id, User updatedUser, CancellationToken ct = default)
        {
             var user = await _repo.GetByIdAsync(id, ct);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found");
            }

            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            user.IsActive = updatedUser.IsActive;
            user.Role = updatedUser.Role;
           
            await _repo.UpdateAsync(user, ct);
        }
    }
}
