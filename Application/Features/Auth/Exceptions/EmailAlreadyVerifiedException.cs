using SurfTicket.Application.Exceptions;

namespace SurfTicket.Application.Features.Auth.Exceptions
{
    public class EmailAlreadyVerifiedException : SurfException
    {
        public EmailAlreadyVerifiedException() : base(SurfErrorCode.USER_ALREADY_CONFIRMED, "User email already confirmed", 400)
        {
        }
    }
}
