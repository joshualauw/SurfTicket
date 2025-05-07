using MediatR;

namespace SurfTicket.Application.Features.Auth.Command.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<UpdateProfileCommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NewEmail { get; set; }
        public string OldEmail { get; set; }
    }
}
