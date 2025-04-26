namespace SurfTicket.DTO
{
    public class ApiResponse<T>
    {
        public int? Status { get; set; } = 200;
        public string Message { get; set; } = string.Empty;
        public bool? Success { get; set; } = true;
        public List<ValidationError>? Errors { get; set; } = new List<ValidationError>();
        public ErrorDetail? Error { get; set; } = null;
        public T? Data { get; set; } = default;

        public ApiResponse(string message, int? status, bool? success, List<ValidationError>? errors, ErrorDetail? detail, T? data)
        {
            Message = message;
            Status = status;
            Success = success;
            Errors = errors;
            Error = detail;
            Data = data;
        }
    }

    public class ErrorDetail
    {
        public string Path { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public object? Detail { get; set; }
    }

    public class ValidationError
    {
        public string Path { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
