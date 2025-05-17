using MediatR;

namespace SurfTicket.Application.Features.Venue.Command.CreateVenue
{
    public class CreateVenueCommand : IRequest<CreateVenueCommandResponse>
    {
        public int MerchantId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
