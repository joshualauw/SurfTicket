using MediatR;

namespace SurfTicket.Application.Features.Auth.Command.VerifyEmail
{
    public class VerifyEmailCommand : IRequest<VerifyEmailCommandResponse>
    {
        public string VerifyCode { get; set; }
        public string Email { get; set; }
    }
}
