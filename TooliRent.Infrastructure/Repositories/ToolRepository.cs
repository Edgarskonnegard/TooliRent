using TooliRent.Domain.Interfaces;
using TooliRent.Domain.Models;
using TooliRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TooliRent.Infrastructure.Repositories
{
    public class ToolRepository : IToolRepository
    {
        private readonly TooliRentContext _context;

        public ToolRepository(TooliRentContext context)
        {
            _context = context;
        }

        public async Task<Tool?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Tools.FirstOrDefaultAsync(t => t.Id == id, ct);
        }

        public async Task<IEnumerable<Tool>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Tools.ToListAsync(ct);
        }

        public async Task AddAsync(Tool tool, CancellationToken ct = default)
        {
            await _context.Tools.AddAsync(tool, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Tool tool, CancellationToken ct = default)
        {
            _context.Tools.Update(tool);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Tool tool, CancellationToken ct = default)
        {
            _context.Tools.Remove(tool);
            await _context.SaveChangesAsync(ct);
        }
    }
}
