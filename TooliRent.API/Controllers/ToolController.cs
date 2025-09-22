using Microsoft.AspNetCore.Mvc;
using TooliRent.Application.DTOs.Tool;
using TooliRent.Application.Interfaces;
using TooliRent.Domain.Models;

namespace TooliRent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolController : ControllerBase
    {
        private readonly IToolService _toolService;

        public ToolController(IToolService toolService)
        {
            _toolService = toolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var tools = await _toolService.GetAllAsync(ct);
            return Ok(tools);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var toolDto = await _toolService.GetByIdAsync(id, ct);
            if (toolDto == null) return NotFound();
            return Ok(toolDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ToolCreateDto toolDto, CancellationToken ct)
        {
            var result = await _toolService.AddAsync(toolDto, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ToolUpdateDto toolDto, CancellationToken ct)
        {
            try
            {
                await _toolService.UpdateAsync(id, toolDto, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            try
            {
                await _toolService.DeleteAsync(id, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}