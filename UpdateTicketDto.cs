using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UpdateTicketDto
    {
        [Required(ErrorMessage = "סטטוס הוא שדה חובה")]
        public string Status { get; set; } = string.Empty;

        public string? ResolutionText { get; set; }
    }
}