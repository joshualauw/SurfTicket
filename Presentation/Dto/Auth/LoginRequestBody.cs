using System.ComponentModel.DataAnnotations;

namespace SurfTicket.Presentation.Dto.Auth
{
    public class LoginRequestBody
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; }
    }
}
