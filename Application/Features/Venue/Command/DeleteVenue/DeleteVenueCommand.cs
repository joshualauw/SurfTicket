using MediatR;

namespace SurfTicket.Application.Features.Venue.Command.DeleteVenue
{
    public class DeleteVenueCommand : IRequest<DeleteVenueCommandResponse>
    {
        public int VenueId { get; set;}
        public int MerchantId { get; set;}
    }
}
