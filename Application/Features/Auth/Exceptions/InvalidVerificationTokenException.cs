using SurfTicket.Application.Exceptions;

namespace SurfTicket.Application.Features.Auth.Exceptions
{
    public class InvalidVerificationTokenException : SurfException
    {
        public InvalidVerificationTokenException() : base(SurfErrorCode.USER_INVALID_VERIFICATION_TOKEN, "Invalid verification token", 400)
        {
        }
    }
}
