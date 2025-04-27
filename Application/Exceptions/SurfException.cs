using SurfTicket.Application.Enums;

namespace SurfTicket.Application.Exceptions
{
    public class SurfException : Exception
    {
        public SurfErrorCode ErrorCode { get; set; }
        public string Handler { get; set; }
        public int StatusCode { get; set; }

        public SurfException(SurfErrorCode errorCode, string message, string handler, int statusCode = 500) : base(message)
        {
            ErrorCode = errorCode;
            Handler = handler;
            StatusCode = statusCode;
        }

        public SurfException(SurfErrorCode errorCode, string message, string handler, int statusCode, Exception? inner) : base(message, inner)
        {
            ErrorCode = errorCode;
            Handler = handler;
            StatusCode = statusCode;
        }
    }

    public class BadRequestSurfException : SurfException
    {
        public BadRequestSurfException(SurfErrorCode errorCode, string message, string handler) : base(errorCode, message, handler, 400) { }
    }

    public class UnauthorizedSurfException : SurfException
    {
        public UnauthorizedSurfException(SurfErrorCode errorCode, string message, string handler) : base(errorCode, message, handler, 401) { }
    }

    public class NotFoundSurfException : SurfException
    {
        public NotFoundSurfException(SurfErrorCode errorCode, string message, string handler) : base(errorCode, message, handler, 404) { }
    }
}
