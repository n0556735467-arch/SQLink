using Entitiies;
using System;

namespace Entities
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? ImagePath { get; set; }

        public TicketStatus Status { get; set; }

        public string? ResolutionText { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string? LastUpdatedBy { get; set; }
    }
}