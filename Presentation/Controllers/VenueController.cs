using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurfTicket.Application.Features.Merchant.Command.CreateMerchant;
using SurfTicket.Application.Features.Venue.Command.CreateVenue;
using SurfTicket.Infrastructure.Dto;
using SurfTicket.Infrastructure.Helpers;
using SurfTicket.Presentation.Dto.Venue;
using SurfTicket.Presentation.Helpers;

namespace SurfTicket.Presentation.Controllers
{
    [Route("api/merchant")]
    [ApiController]
    [Authorize]
    public class VenueController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISender _sender;
        public VenueController(IConfiguration configuration, ISender sender)
        {
            _configuration = configuration;
            _sender = sender;
        }

        [HttpPost("{merchantId}")]
        public async Task<IActionResult> CreateVenue(int merchantId, [FromBody] CreateVenueBody body)
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            CreateVenueCommand command = new CreateVenueCommand()
            {
               MerchantId = merchantId,
               UserId = user.UserId,
               Name = body.Name,
               Description = body.Description,
            };

            var result = await _sender.Send(command);

            return Ok(ApiResponseHelper.Success("Create venue succesful", result));
        }
    }
}
