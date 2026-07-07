using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CreateTicketDto
    {
        [Required(ErrorMessage = "שם מלא הוא שדה חובה")]
        [StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "אימייל הוא שדה חובה")]
        [EmailAddress(ErrorMessage = "כתובת אימייל לא תקינה")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "תיאור התקלה הוא שדה חובה")]
        [StringLength(2000, MinimumLength = 5)]
        public string Description { get; set; } = string.Empty;
    }
}