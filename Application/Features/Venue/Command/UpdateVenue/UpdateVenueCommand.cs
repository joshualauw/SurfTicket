using MediatR;

namespace SurfTicket.Application.Features.Venue.Command.UpdateVenue
{
    public class UpdateVenueCommand : IRequest<UpdateVenueCommandResponse>
    {
        public int MerchantId { get; set; }
        public int VenueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
