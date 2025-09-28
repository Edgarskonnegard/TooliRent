using Microsoft.AspNetCore.Mvc;
using TooliRent.Domain.Models;
using TooliRent.Application.Interfaces;
using TooliRent.Application.DTOs.Category;
using Microsoft.AspNetCore.Authorization;

namespace TooliRent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var categories = await _categoryService.GetAllAsync(ct);
            return Ok(categories);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var category = await _categoryService.GetByIdAsync(id, ct);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto categoryDto, CancellationToken ct)
        {
            var newCategory = await _categoryService.AddAsync(categoryDto, ct);
            return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto categoryDto, CancellationToken ct)
        {
            try
            {
                await _categoryService.UpdateAsync(id, categoryDto, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            try
            {
                await _categoryService.DeleteAsync(id, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
