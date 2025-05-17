using System.ComponentModel.DataAnnotations;

namespace SurfTicket.Presentation.Dto.Venue
{
    public class CreateVenueBody
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
