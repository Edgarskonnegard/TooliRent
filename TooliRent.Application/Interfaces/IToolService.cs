using TooliRent.Application.DTOs.Tool;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Interfaces;

public interface IToolService
{
    Task<ToolReadDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<ToolReadDto?>> GetAllAsync(CancellationToken ct = default);
    Task<ToolReadDto?> AddAsync(ToolCreateDto toolDto, CancellationToken ct = default);
    Task<ToolReadDto?> UpdateAsync(int id, ToolUpdateDto updatedToolDto, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
