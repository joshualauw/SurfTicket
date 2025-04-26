using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SurfTicket.DTO;
using SurfTicket.Helpers;

namespace SurfTicket.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Exception Occurred.");

            int statusCode = 500;
            if (context.Exception is HttpRequestException httpRequest)
            {
                if (httpRequest.StatusCode.HasValue)
                {
                    statusCode = (int)httpRequest.StatusCode.Value;
                }
            }

            var response = ApiResponseHelper.Error(context.Exception.Message, new ErrorDetail()
            {
                Path = context.HttpContext.Request.Path,
                Source = context.Exception.Source ?? "Unknown Source",
                Detail = context.Exception.Data.Contains("errorDetail")  ? context.Exception.Data["errorDetail"] : null
            }, statusCode);

            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode,
            };

            context.ExceptionHandled = true;
        }
    }
}
