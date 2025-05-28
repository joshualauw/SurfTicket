using MediatR;
using SurfTicket.Infrastructure.Common;

namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenues
{
    public class GetAdminVenuesQuery : IRequest<GetAdminVenuesQueryResponse>
    {
        public int MerchantId { get; set; }
        public FilterQuery Filter { get; set; }
    }
}
