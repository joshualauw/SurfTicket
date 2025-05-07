﻿using System.ComponentModel.DataAnnotations;

namespace SurfTicket.Presentation.Dto.Auth
{
    public class RegisterBody
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string PasswordConfirmation { get; set; }
    }
}
