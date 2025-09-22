using TooliRent.Application.DTOs.Category;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Interfaces;

public interface ICategoryService
{
    Task<CategoryReadDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<CategoryReadDto?>> GetAllAsync(CancellationToken ct = default);
    Task<CategoryReadDto?> AddAsync(CategoryCreateDto categoryDto, CancellationToken ct = default);
    Task<CategoryReadDto?> UpdateAsync(int id, CategoryUpdateDto updatedCategoryDto, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
