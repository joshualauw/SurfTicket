using System.ComponentModel.DataAnnotations;

namespace SurfTicket.Presentation.Dto.Venue
{
    public class UploadVenueLogoBody
    {
        [Required(ErrorMessage = "Logo is required")]
        public IFormFile Logo { get; set; }
    }
}
