using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SurfTicket.DTO;
using SurfTicket.Helpers;

namespace SurfTicket.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Keys
                    .SelectMany(key => context.ModelState[key]!.Errors
                        .Select(error => new ValidationError
                        {
                            Path = key,
                            Message = error.ErrorMessage,
                        })
                    ).ToList();


                var response = ApiResponseHelper.ValidationError(errors, new ErrorDetail()
                {
                    Path = context.HttpContext.Request.Path,
                    Source = "Validation",
                    Detail = "{}"
                });

                context.Result = new BadRequestObjectResult(response);
            }
        }
    }   
}
