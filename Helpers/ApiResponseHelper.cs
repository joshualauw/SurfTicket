using SurfTicket.DTO;

namespace SurfTicket.Helpers
{
    public class ApiResponseHelper
    {
        public static ApiResponse<T> Success<T>(string message, T? data = default, int status = 200)
        {
            return new ApiResponse<T>(message, status, true, new List<ValidationError>(), null, data);
        }

        public static ApiResponse<object> Error(string message, ErrorDetail? error, int status = 500)
        {
            return new ApiResponse<object>(message, status, false, new List<ValidationError>(), error, default);
        }

        public static ApiResponse<object> ValidationError(List<ValidationError> errors, ErrorDetail? error)
        {
            return new ApiResponse<object>("validation failed", 400, false, errors, error, default);
        }
    }
}
