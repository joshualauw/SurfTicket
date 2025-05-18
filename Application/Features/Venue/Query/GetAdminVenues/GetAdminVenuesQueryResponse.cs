using SurfTicket.Application.Features.Venue.Query.GetAdminVenues.Dto;

namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenues
{
    public class GetAdminVenuesQueryResponse
    {
        public List<AdminVenueItem> Venues { get; set; }
    }
}
