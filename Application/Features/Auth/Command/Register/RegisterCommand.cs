using MediatR;

namespace SurfTicket.Application.Features.Auth.Command.Register
{
    public class RegisterCommand : IRequest<RegisterCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
