using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurfTicket.Application.Features.Merchant.Command.CreateMerchant;
using SurfTicket.Application.Features.Merchant.Query.GetHandlerMerchants;
using SurfTicket.Infrastructure.Dto;
using SurfTicket.Infrastructure.Helpers;
using SurfTicket.Presentation.Dto.Merchant;
using SurfTicket.Presentation.Helpers;

namespace SurfTicket.Presentation.Controllers
{
    [Route("api/merchant")]
    [ApiController]
    [Authorize]
    public class MerchantController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISender _sender;
        public MerchantController(IConfiguration configuration, ISender sender)
        {
            _configuration = configuration;
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetHandledMerchants()
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            GetHandledMerchantsQuery query = new GetHandledMerchantsQuery()
            {
                UserId = user.UserId,
            };

            var result = await _sender.Send(query);

            return Ok(ApiResponseHelper.Success("Get handled merchants successful", result));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMerchant([FromBody] CreateMerchantBody body)
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            CreateMerchantCommand command = new CreateMerchantCommand()
            {
                Name = body.Name,
                Description = body.Description,
                UserId = user.UserId,
            };

            var result = await _sender.Send(command);

            return Ok(ApiResponseHelper.Success("Create merchant succesful", result));
        }
    }
}
