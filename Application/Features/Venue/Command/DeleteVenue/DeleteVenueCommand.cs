using MediatR;

namespace SurfTicket.Application.Features.Venue.Command.DeleteVenue
{
    public class DeleteVenueCommand : IRequest<DeleteVenueCommandResponse>
    {
        public string UserId { get; set; }
        public int VenueId { get; set;}
        public int MerchantId { get; set;}
    }
}
