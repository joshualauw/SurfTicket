namespace SurfTicket.Application.Exceptions
{
    public class SurfException : Exception
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
}
