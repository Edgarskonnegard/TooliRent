using TooliRent.Domain.Models;

namespace TooliRent.Domain.Interfaces
{
    public interface IToolRepository
    {
        Task<Tool?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<Tool>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Tool tool, CancellationToken ct = default);
        Task UpdateAsync(Tool tool, CancellationToken ct = default);
        Task DeleteAsync(Tool tool, CancellationToken ct = default);
    }
}
