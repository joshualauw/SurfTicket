using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurfTicket.Application.Features.Merchant.Query.GetHandlerMerchants;
using SurfTicket.Application.Features.Merchant.Query.GetMerchantUser;
using SurfTicket.Infrastructure.Dto;
using SurfTicket.Infrastructure.Helpers;
using SurfTicket.Presentation.Helpers;

namespace SurfTicket.Presentation.Controllers
{
    [Route("api/merchant-user")]
    [ApiController]
    [Authorize]
    public class MerchantUserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISender _sender;

        public MerchantUserController(IConfiguration configuration, ISender sender)
        {
            _configuration = configuration;
            _sender = sender;
        }

        [HttpGet("{merchantId}")]
        public async Task<IActionResult> GetMerchantUser(int merchantId)
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            GetMerchantUserQuery query = new GetMerchantUserQuery()
            {
                UserId = user.UserId,
                MerchantId = merchantId
            };

            var result = await _sender.Send(query);

            return Ok(ApiResponseHelper.Success("Get merchant user successful", result));
        }
    }
}
