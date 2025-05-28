using SurfTicket.Infrastructure.Dto;

namespace SurfTicket.Application.Features.Auth.Command.Me
{
    public class MeCommandResponse
    {
        public UserJwtPayload User { get; set; }
    }
}
