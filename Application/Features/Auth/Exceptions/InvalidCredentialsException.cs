using SurfTicket.Application.Exceptions;

namespace SurfTicket.Application.Features.Auth.Exceptions
{
    public class InvalidCredentialsException : SurfException
    {
        public InvalidCredentialsException(SurfErrorCode errorCode) : base(errorCode, "Invalid credentials", 401)
        {
        }
    }
}
