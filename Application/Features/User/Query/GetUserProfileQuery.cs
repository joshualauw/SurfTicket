using MediatR;

namespace SurfTicket.Application.Features.User.Query
{
    public class GetUserProfileQuery : IRequest<GetUserProfileQueryResponse>
    {
        public string UserId { get; set; }
    }
}
