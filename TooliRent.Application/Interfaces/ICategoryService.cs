using TooliRent.Domain.Models;

namespace TooliRent.Application.Interfaces;

public interface ICategoryService
{
    Task<Category?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Category category, CancellationToken ct = default);
    Task UpdateAsync(int id, Category updatedCategory, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
