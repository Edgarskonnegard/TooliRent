using TooliRent.Domain.Interfaces;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default)
        {
            return await _repo.GetAllAsync(ct);
        }

        public async Task<Category?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _repo.GetByIdAsync(id, ct);
        }

        public async Task AddAsync(Category category, CancellationToken ct = default)
        {
            await _repo.AddAsync(category, ct);
        }

        public async Task UpdateAsync(Category category, CancellationToken ct = default)
        {
            await _repo.UpdateAsync(category, ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var category = await _repo.GetByIdAsync(id, ct);
            if (category != null)
            {
                await _repo.DeleteAsync(category, ct);
            }
        }
    }
}
