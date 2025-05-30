using MediatR;

namespace SurfTicket.Application.Features.Venue.Command.UploadVenueLogo
{
    public class UploadVenueLogoCommand : IRequest<UploadVenueLogoCommandResponse>
    {
        public int MerchantId { get; set; }
        public int VenueId { get; set; }
        public IFormFile File { get; set; }
    }
}
