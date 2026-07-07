using Entities;
using Entitiies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.interfaces
{
    public interface ITicketRepository
    {
        Task AddAsync(Ticket ticket);
        Task<Ticket?> GetByIdAsync(Guid id);
        Task<List<Ticket>> GetAllAsync();
        Task<List<Ticket>> GetFilteredAsync(TicketStatus? status, string? search);

        Task SaveChangesAsync();
    }
}
