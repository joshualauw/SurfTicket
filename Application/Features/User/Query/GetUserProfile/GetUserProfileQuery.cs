using MediatR;

namespace SurfTicket.Application.Features.User.Query.GetUserProfile
{
    public class GetUserProfileQuery : IRequest<GetUserProfileQueryResponse>
    {
        public string UserId { get; set; }
    }
}
