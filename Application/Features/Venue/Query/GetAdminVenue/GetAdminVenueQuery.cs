using MediatR;

namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenue
{
    public class GetAdminVenueQuery : IRequest<GetAdminVenueQueryResponse>
    {
        public string UserId { get; set; }
        public int MerchantId { get; set; }
        public int VenueId { get; set; }
    }
}
