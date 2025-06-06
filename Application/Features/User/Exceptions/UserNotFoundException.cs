using SurfTicket.Application.Exceptions;

namespace SurfTicket.Application.Features.User.Exceptions
{
    public class UserNotFoundException : SurfException
    {
        public UserNotFoundException() : base(SurfErrorCode.USER_NOT_FOUND, "User not found", 404) { }
    }
}
