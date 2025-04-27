using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SurfTicket.Presentation.Dto;
using SurfTicket.Presentation.Helpers;

namespace SurfTicket.Presentation.Attributes
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


                var response = ApiResponseHelper.ValidationError(errors);
                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
