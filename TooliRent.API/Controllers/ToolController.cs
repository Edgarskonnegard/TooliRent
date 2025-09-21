using Microsoft.AspNetCore.Mvc;
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
            var tool = await _toolService.GetByIdAsync(id, ct);
            if (tool == null) return NotFound();
            return Ok(tool);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tool tool, CancellationToken ct)
        {
            await _toolService.AddAsync(tool, ct);
            return CreatedAtAction(nameof(GetById), new { id = tool.Id }, tool);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Tool tool, CancellationToken ct)
        {
            try
            {
                await _toolService.UpdateAsync(id, tool, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}