using SurfTicket.Application.Exceptions;

namespace SurfTicket.Application.Features.Auth.Exceptions
{
    public class EmailAlreadyUsedException : SurfException
    {
        public EmailAlreadyUsedException() : base(SurfErrorCode.USER_EMAIL_ALREADY_USED, "Email already taken", 400)
        {
        }
    }
}
