using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.interfaces
{
    public interface ITicketService
    {
        Task<Guid> CreateTicketAsync(CreateTicketDto dto, string? imagePath);
        Task<TicketResponseDto?> GetTicketByIdAsync(Guid id);
        Task<List<TicketResponseDto>> GetFilteredTicketsAsync(string? status, string? search);
        Task<bool> UpdateTicketAsync(Guid id, UpdateTicketDto dto, string updatedBy, bool isAdmin);

    }
}
