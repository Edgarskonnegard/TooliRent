using TooliRent.Application.Interfaces;
using TooliRent.Domain.Interfaces;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Services;

public class ToolService : IToolService
{
    private readonly IToolRepository _toolRepository;

    public ToolService(IToolRepository toolRepository)
    {
        _toolRepository = toolRepository;
    }

    public async Task<Tool?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _toolRepository.GetByIdAsync(id, ct);
    }

    public async Task<IEnumerable<Tool>> GetAllAsync(CancellationToken ct = default)
    {
        return await _toolRepository.GetAllAsync(ct);
    }

    public async Task AddAsync(Tool tool, CancellationToken ct = default)
    {
        await _toolRepository.AddAsync(tool, ct);
    }

    public async Task UpdateAsync(Tool tool, CancellationToken ct = default)
    {
        await _toolRepository.UpdateAsync(tool, ct);
    }

    public async Task DeleteAsync(Tool tool, CancellationToken ct = default)
    {
        await _toolRepository.DeleteAsync(tool, ct);
    }
}
