using AutoMapper;
using TooliRent.Application.DTOs.Category;
using TooliRent.Application.Interfaces;
using TooliRent.Domain.Interfaces;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryReadDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var result = await _categoryRepository.GetByIdAsync(id, ct);
        return _mapper.Map<CategoryReadDto>(result);
    }

    public async Task<IEnumerable<CategoryReadDto?>> GetAllAsync(CancellationToken ct = default)
    {
        var result = await _categoryRepository.GetAllAsync(ct);
        return _mapper.Map<IEnumerable<CategoryReadDto>>(result);
    }

    public async Task<CategoryReadDto?> AddAsync(CategoryCreateDto categoryDto, CancellationToken ct = default)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.AddAsync(category, ct);
        return _mapper.Map<CategoryReadDto>(category);
    }

    public async Task<CategoryReadDto?> UpdateAsync(int id, CategoryUpdateDto updatedCategoryDto, CancellationToken ct = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id, ct);

        if (category == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found");
        }

        category.Name = updatedCategoryDto.Name;

        await _categoryRepository.UpdateAsync(category, ct);
        return _mapper.Map<CategoryReadDto>(category);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var categoryToDelete = await _categoryRepository.GetByIdAsync(id, ct);
        if(categoryToDelete == null) throw new KeyNotFoundException();

        await _categoryRepository.DeleteAsync(categoryToDelete, ct);
    }
}
