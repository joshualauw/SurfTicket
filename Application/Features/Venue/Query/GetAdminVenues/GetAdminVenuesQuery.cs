using MediatR;
using SurfTicket.Application.Common;
using SurfTicket.Application.Features.Venue.Query.GetAdminVenues.Dto;

namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenues
{
    public class GetAdminVenuesQuery : IRequest<PagedResult<AdminVenueItem>>
    {
        public string UserId { get; set; }
        public int MerchantId { get; set; }
        public PaginationQuery Pagination { get; set; }
    }
}
