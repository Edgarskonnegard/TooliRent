using AutoMapper;
using TooliRent.Application.DTOs.User;
using TooliRent.Application.Interfaces;
using TooliRent.Domain.Interfaces;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public UserService(IUserRepository repo, IMapper mapper, IPasswordService passwordService)
        {
            _repo = repo;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<UserReadDto?> AddAsync(UserCreateDto userDto, CancellationToken ct = default)
        {
            var user = _mapper.Map<User>(userDto);
            user.Role = "Member";
            user.PasswordHash = _passwordService.HashPassword(user, userDto.Password);

            await _repo.AddAsync(user, ct);
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto?> AddStaffAsync(UserCreateDto userDto, CancellationToken ct = default)
        {
            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = _passwordService.HashPassword(user, userDto.Password);

            await _repo.AddAsync(user, ct);
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user != null)
            {
                await _repo.DeleteAsync(user, ct);
            }
        }

        public async Task<IEnumerable<UserReadDto?>> GetAllAsync(CancellationToken ct = default)
        {
            var users = await _repo.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            var userEmail = await _repo.GetByEmailAsync(email, ct);
            return _mapper.Map<UserReadDto>(userEmail);
        }

        public async Task<UserReadDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var user = await _repo.GetByIdAsync(id, ct);
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto?> UpdateAsync(int id, UserUpdateDto updatedUserDto, CancellationToken ct = default)
        {
            var user = await _repo.GetByIdAsync(id, ct);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found");
            }

            user.Username = updatedUserDto.Username;
            user.Email = updatedUserDto.Email;
            user.IsActive = true;
            user.Role = "Member";

            await _repo.UpdateAsync(user, ct);
            return _mapper.Map<UserReadDto>(user);
        }
    }
}
