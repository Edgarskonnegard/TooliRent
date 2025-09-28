using Microsoft.AspNetCore.Mvc;
using TooliRent.Domain.Models;
using TooliRent.Application.Interfaces;
using TooliRent.Application.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TooliRent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var users = await _userService.GetAllAsync(ct);
            return Ok(users);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var user = await _userService.GetByIdAsync(id, ct);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto userDto, CancellationToken ct)
        {
            var result = await _userService.AddAsync(userDto, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize("admin")]
        [HttpPost("{Staff}")]
        public async Task<IActionResult> CreateNewStaff(UserCreateDto userDto, CancellationToken ct)
        {
            var result = await _userService.AddStaffAsync(userDto, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize(Roles = "Member,admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto userDto, CancellationToken ct)
        {
            var loggedInUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var loggedInUserRole = User.FindFirst(ClaimTypes.Role)!.Value;

            if (loggedInUserRole == "Member" && loggedInUserId != id)
            {
                return Forbid(); 
            }
            try
            {
                await _userService.UpdateAsync(id, userDto, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Member,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {   
            var loggedInUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var loggedInUserRole = User.FindFirst(ClaimTypes.Role)!.Value;

            if (loggedInUserRole == "Member" && loggedInUserId != id)
            {
                return Forbid(); 
            }
            try
            {
                await _userService.DeleteAsync(id, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}