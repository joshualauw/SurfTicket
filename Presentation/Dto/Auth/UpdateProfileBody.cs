﻿using System.ComponentModel.DataAnnotations;

namespace SurfTicket.Presentation.Dto.Auth
{
    public class UpdateProfileBody
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address")]
        public string Email { get; set; }
    }
}
