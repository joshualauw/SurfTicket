using SurfTicket.Application.Enums;

namespace SurfTicket.Application.Exceptions
{
    public abstract class SurfException : Exception
    {
        public SurfErrorCode ErrorCode { get; set; }
        public int StatusCode { get; set; }

        public SurfException(SurfErrorCode errorCode, string message, int statusCode) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }

        public SurfException(SurfErrorCode errorCode, string message, int statusCode, Exception? inner) : base(message, inner)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }

    public class InternalSurfException : SurfException
    {
        public InternalSurfException(SurfErrorCode errorCode, string message) : base(errorCode, message, 500) { }
    }

    public class BadRequestSurfException : SurfException
    {
        public BadRequestSurfException(SurfErrorCode errorCode, string message) : base(errorCode, message, 400) { }
    }

    public class UnprocessableSurfException : SurfException
    {
        public UnprocessableSurfException(SurfErrorCode errorCode, string message) : base(errorCode, message, 422) { }
    }

    public class UnauthorizedSurfException : SurfException
    {
        public UnauthorizedSurfException(SurfErrorCode errorCode, string message) : base(errorCode, message, 401) { }
    }

    public class NotFoundSurfException : SurfException
    {
        public NotFoundSurfException(SurfErrorCode errorCode, string message) : base(errorCode, message, 404) { }
    }
}
