using System.ComponentModel.DataAnnotations;

namespace SurfTicket.Presentation.Dto.Auth
{
    public class VerifyEmailBody
    {
        [Required(ErrorMessage = "Verify code is required")]
        public string VerifyCode { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address")]
        public string Email { get; set; }
    }
}
