using System.ComponentModel.DataAnnotations;

namespace SurfTicket.Presentation.Dto.Merchant
{
    public class CreateMerchantBody
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Description cannot be empty")]
        public string? Description { get; set; }
    }
}
