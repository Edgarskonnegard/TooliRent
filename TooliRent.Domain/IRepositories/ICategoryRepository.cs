using TooliRent.Domain.Models;

namespace TooliRent.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Category category, CancellationToken ct = default);
        Task UpdateAsync(Category category, CancellationToken ct = default);
        Task DeleteAsync(Category category, CancellationToken ct = default);
    }
}
