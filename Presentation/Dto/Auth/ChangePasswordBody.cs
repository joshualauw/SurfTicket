using System.ComponentModel.DataAnnotations;

namespace SurfTicket.Presentation.Dto.Auth
{
    public class ChangePasswordBody
    {
        [Required(ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string PasswordConfirmation { get; set; }
    }
}
