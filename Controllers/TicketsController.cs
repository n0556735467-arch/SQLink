using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.interfaces;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromForm] CreateTicketDto dto, IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? imagePath = null;
            if (image != null)
            {
                imagePath = await SaveImageToDiskAsync(image);
            }

            var ticketId = await _ticketService.CreateTicketAsync(dto, imagePath);

            return CreatedAtAction(nameof(GetTicketById), new { id = ticketId }, new { id = ticketId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(Guid id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        private async Task<string> SaveImageToDiskAsync(IFormFile image)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder);
            var fileName = $"{Guid.NewGuid()}_{image.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return $"/uploads/{fileName}";
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTickets([FromQuery] string? status, [FromQuery] string? search)
        {
            try
            {
                var tickets = await _ticketService.GetFilteredTicketsAsync(status, search);
                return Ok(tickets);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] UpdateTicketDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown";
            var isAdmin = User.IsInRole("Admin");

            try
            {
                var success = await _ticketService.UpdateTicketAsync(id, dto, userEmail, isAdmin);

                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return ex is UnauthorizedAccessException
                       ? Forbid()
                       : BadRequest(ex.Message);
            }
        }
    }
}