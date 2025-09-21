using TooliRent.Domain.Models;

namespace TooliRent.Application.Interfaces;

public interface IToolService
{
    Task<Tool?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Tool>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Tool tool, CancellationToken ct = default);
    Task UpdateAsync(int id, Tool updatedTool, CancellationToken ct = default);
    Task DeleteAsync(Tool tool, CancellationToken ct = default);
}
