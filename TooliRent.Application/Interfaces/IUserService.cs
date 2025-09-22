using TooliRent.Application.DTOs.User;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserReadDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<UserReadDto?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<IEnumerable<UserReadDto?>> GetAllAsync(CancellationToken ct = default);
        Task<UserReadDto?> AddAsync(UserCreateDto userDto, CancellationToken ct = default);
        Task<UserReadDto?> UpdateAsync(int id, UserUpdateDto updatedUserDto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
