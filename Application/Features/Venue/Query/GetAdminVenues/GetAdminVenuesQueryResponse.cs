using SurfTicket.Application.Features.Venue.Query.GetAdminVenues.Dto;
using SurfTicket.Infrastructure.Common;

namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenues
{
    public class GetAdminVenuesQueryResponse
    {
        public PagedData<AdminVenueItem> Venues { get; set; }
    }
}
