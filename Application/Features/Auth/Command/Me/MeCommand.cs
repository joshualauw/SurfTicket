using MediatR;

namespace SurfTicket.Application.Features.Auth.Command.Me
{
    public class MeCommand : IRequest<MeCommandResponse>
    {
        public string UserId { get; set; }
    }
}
