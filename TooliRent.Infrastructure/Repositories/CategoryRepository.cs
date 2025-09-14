using Microsoft.EntityFrameworkCore;
using TooliRent.Domain.Interfaces;
using TooliRent.Domain.Models;
using TooliRent.Infrastructure.Data;

namespace TooliRent.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TooliRentContext _context;

        public CategoryRepository(TooliRentContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Categories
                    .Include(c => c.Tools)
                    .FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Categories
                    .Include(c => c.Tools)
                    .ToListAsync(ct);
        }

        public async Task AddAsync(Category category, CancellationToken ct = default)
        {
            await _context.Categories.AddAsync(category, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Category category, CancellationToken ct = default)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Category category, CancellationToken ct = default)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(ct);
        }
    }
}
