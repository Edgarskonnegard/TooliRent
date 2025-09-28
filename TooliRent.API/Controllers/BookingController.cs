using Microsoft.AspNetCore.Mvc;
using TooliRent.Domain.Models;
using TooliRent.Application.Interfaces;
using TooliRent.Application.DTOs.Booking;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TooliRent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var bookings = await _bookingService.GetAllAsync(ct);
            return Ok(bookings);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var booking = await _bookingService.GetByIdAsync(id, ct);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [Authorize(Roles = "Member, admin")]
        [HttpPost]
        public async Task<IActionResult> Create(BookingCreateDto bookingDto, CancellationToken ct)
        {
            var createdBooking = await _bookingService.AddAsync(bookingDto, ct);
            return CreatedAtAction(nameof(GetById), new { id = createdBooking.Id }, createdBooking);
        }

        [Authorize(Roles = "Member, admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookingUpdateDto bookingDto, CancellationToken ct)
        {
            try
            {
                await _bookingService.UpdateAsync(id, bookingDto, ct);
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
                await _bookingService.DeleteAsync(id, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}/collect")]
        public async Task<IActionResult> Collect(int id, CancellationToken ct)
        {
            try
            {
                var booking = await _bookingService.CollectAsync(id, ct);
                return Ok(booking);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}/return")]
        public async Task<IActionResult> Return(int id, CancellationToken ct)
        {
            try
            {
                var booking = await _bookingService.ReturnAsync(id, ct);
                return Ok(booking);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Member,Admin")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyBookings(CancellationToken ct)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var bookings = await _bookingService.GetByUserIdAsync(userId, ct);

            return Ok(bookings);
        }

        [Authorize(Roles = "Member, admin")]
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id, CancellationToken ct)
        {
            var booking = await _bookingService.GetByIdAsync(id, ct);
            if (booking == null)
                return NotFound();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();

            int currentUserId = int.Parse(userIdClaim);

            if (booking.UserId != currentUserId && !User.IsInRole("admin"))
                return Forbid();
            try
            {
                await _bookingService.CancelAsync(id, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "admin")] // eller receptionist/admin
        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdue(CancellationToken ct)
        {
            var overdueBookings = await _bookingService.GetOverdueAsync(ct);
            return Ok(overdueBookings);
        }



    }
}
