using MediatR;

namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenue
{
    public class GetAdminVenueQuery : IRequest<GetAdminVenueQueryResponse>
    {
        public int MerchantId { get; set; }
        public int VenueId { get; set; }
    }
}
