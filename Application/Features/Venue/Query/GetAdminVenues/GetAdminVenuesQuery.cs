using MediatR;

namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenues
{
    public class GetAdminVenuesQuery : IRequest<GetAdminVenusQueryResponse>
    {
        public string UserId { get; set; }
        public int MerchantId { get; set; }
    }
}
