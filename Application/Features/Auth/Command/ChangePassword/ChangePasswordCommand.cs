using MediatR;

namespace SurfTicket.Application.Features.Auth.Command.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ChangePasswordCommandResponse>
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
