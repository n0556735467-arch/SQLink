using DTOs;
using Entities;
using Entitiies;
using Repositories;
using Repositories.interfaces;
using Services.interfaces;

namespace Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;

        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateTicketAsync(CreateTicketDto dto, string? imagePath)
        {
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                Description = dto.Description,
                ImagePath = imagePath,
                Status = TicketStatus.Open,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(ticket);
            await _repository.SaveChangesAsync();

            return ticket.Id;
        }

        public async Task<TicketResponseDto?> GetTicketByIdAsync(Guid id)
        {
            var ticket = await _repository.GetByIdAsync(id);
            if (ticket == null)
            {
                return null;
            }

            return MapToDto(ticket);
        }

        private static TicketResponseDto MapToDto(Ticket ticket)
        {
            return new TicketResponseDto
            {
                Id = ticket.Id,
                FullName = ticket.FullName,
                Email = ticket.Email,
                Description = ticket.Description,
                ImagePath = ticket.ImagePath,
                Status = ticket.Status.ToString(),
                ResolutionText = ticket.ResolutionText,
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt,
                LastUpdatedBy = ticket.LastUpdatedBy
            };
        }
        public async Task<List<TicketResponseDto>> GetFilteredTicketsAsync(string? status, string? search)
        {
            TicketStatus? parsedStatus = null;

            if (!string.IsNullOrWhiteSpace(status))
            {
                if (!Enum.TryParse<TicketStatus>(status, ignoreCase: true, out var result))
                {
                    throw new ArgumentException($"סטטוס לא חוקי: {status}");
                }
                parsedStatus = result;
            }

            var tickets = await _repository.GetFilteredAsync(parsedStatus, search);

            return tickets.Select(MapToDto).ToList();
        }

        public async Task<bool> UpdateTicketAsync(Guid id, UpdateTicketDto dto, string updatedBy, bool isAdmin)
        {
            var ticket = await _repository.GetByIdAsync(id);
            if (ticket == null)
            {
                return false;
            }

            if (ticket.Status == TicketStatus.Closed && !isAdmin)
            {
                throw new UnauthorizedAccessException("רק Admin יכול לעדכן טיקט בסטטוס Closed");
            }

            if (!Enum.TryParse<TicketStatus>(dto.Status, ignoreCase: true, out var newStatus))
            {
                throw new ArgumentException($"סטטוס לא חוקי: {dto.Status}");
            }

            if (newStatus == TicketStatus.Resolved && string.IsNullOrWhiteSpace(dto.ResolutionText))
            {
                throw new InvalidOperationException("מעבר לסטטוס Resolved מחייב טקסט פתרון");
            }

            ticket.Status = newStatus;
            ticket.ResolutionText = dto.ResolutionText;
            ticket.UpdatedAt = DateTime.UtcNow;
            ticket.LastUpdatedBy = updatedBy;

            await _repository.SaveChangesAsync();
            return true;
        }
    }
}