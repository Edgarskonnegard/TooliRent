using TooliRent.Application.Interfaces;
using TooliRent.Domain.Interfaces;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _categoryRepository.GetByIdAsync(id, ct);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default)
    {
        return await _categoryRepository.GetAllAsync(ct);
    }

    public async Task AddAsync(Category category, CancellationToken ct = default)
    {
        await _categoryRepository.AddAsync(category, ct);
    }

    public async Task UpdateAsync(Category category, CancellationToken ct = default)
    {
        await _categoryRepository.UpdateAsync(category, ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var categoryToDelete = await _categoryRepository.GetByIdAsync(id, ct);
        if(categoryToDelete == null) throw new KeyNotFoundException();
        
        await _categoryRepository.DeleteAsync(categoryToDelete, ct);
    }
}
