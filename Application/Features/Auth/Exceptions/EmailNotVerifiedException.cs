using SurfTicket.Application.Exceptions;

namespace SurfTicket.Application.Features.Auth.Exceptions
{
    public class EmailNotVerifiedException : SurfException
    {
        public EmailNotVerifiedException() : base(SurfErrorCode.USER_NOT_CONFIRMED, "User email not verified", 401)
        {
        }
    }
}
