using Newtonsoft.Json;
using SurfTicket.Presentation.Helpers;
using SurfTicket.Presentation.Dto;
using SurfTicket.Application.Exceptions;
using Newtonsoft.Json.Serialization;

namespace SurfTicket.Presentation.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SurfException ex)
            {
                _logger.LogError(ex, "Application Error - ErrorCode: {ErrorCode}, Message: {Message}, InnerException: {InnerException}, Path: {Path}, Source: {Source}",
                    ex.ErrorCode, ex.Message, ex.InnerException, context.Request.Path, ex.Source);

                 await ErrorJsonResponse(context, ex.Message, ex.StatusCode, (int) ex.ErrorCode);          
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Generic Error - Message: {Message}, InnerException: {InnerException}, Path: {Path}, Source: {Source}",
                    ex.Message, ex.InnerException, context.Request.Path, ex.Source);

                await ErrorJsonResponse(context, "Something went wrong", 500, (int) SurfErrorCode.GENERIC_ERROR);
            }
        }

        private async Task ErrorJsonResponse(HttpContext context, string message, int statusCode, int errorCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(ApiResponseHelper.Error($"(#{errorCode}) - {message}", new ErrorDetail()
            {
                ErrorCode = errorCode,
                ErrorMessage = message,
            }, statusCode), settings);

            await context.Response.WriteAsync(json);
        }
    }
}
