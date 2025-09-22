using AutoMapper;
using TooliRent.Application.DTOs.Tool;
using TooliRent.Application.Interfaces;
using TooliRent.Domain.Interfaces;
using TooliRent.Domain.Models;

namespace TooliRent.Application.Services;

public class ToolService : IToolService
{
    private readonly IToolRepository _toolRepository;
    private readonly IMapper _mapper;

    public ToolService(IToolRepository toolRepository, IMapper mapper)
    {
        _toolRepository = toolRepository;
        _mapper = mapper;
    }

    public async Task<ToolReadDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var result = await _toolRepository.GetByIdAsync(id, ct);
        return _mapper.Map<ToolReadDto>(result);
    }

    public async Task<IEnumerable<ToolReadDto?>> GetAllAsync(CancellationToken ct = default)
    {
        var result = await _toolRepository.GetAllAsync(ct);
        return _mapper.Map<IEnumerable<ToolReadDto>>(result);
    }

    public async Task<ToolReadDto?> AddAsync(ToolCreateDto toolDto, CancellationToken ct = default)
    {
        var tool = _mapper.Map<Tool>(toolDto);
        await _toolRepository.AddAsync(tool, ct);
        return _mapper.Map<ToolReadDto>(tool);
    }

    public async Task<ToolReadDto?> UpdateAsync(int id, ToolUpdateDto updatedToolDto, CancellationToken ct = default)
    {
        var existingTool = await _toolRepository.GetByIdAsync(id, ct);
        if (existingTool == null) throw new KeyNotFoundException("Tool not found");


        existingTool.Name = updatedToolDto.Name;
        existingTool.CategoryId = updatedToolDto.CategoryId;
        existingTool.Description = updatedToolDto.Description;
        existingTool.IsAvailable = updatedToolDto.IsAvailable;

        await _toolRepository.UpdateAsync(existingTool, ct);
        return _mapper.Map<ToolReadDto>(existingTool);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var toolToDelete = await _toolRepository.GetByIdAsync(id, ct);
        if( toolToDelete == null) throw new KeyNotFoundException();

        await _toolRepository.DeleteAsync(toolToDelete, ct);
    }
}
