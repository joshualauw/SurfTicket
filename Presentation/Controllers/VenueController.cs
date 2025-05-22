using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurfTicket.Infrastructure.Common;
using SurfTicket.Application.Features.Venue.Command.CreateVenue;
using SurfTicket.Application.Features.Venue.Query.GetAdminVenues;
using SurfTicket.Infrastructure.Dto;
using SurfTicket.Infrastructure.Helpers;
using SurfTicket.Presentation.Dto.Venue;
using SurfTicket.Presentation.Helpers;

namespace SurfTicket.Presentation.Controllers
{
    [Route("api/venue")]
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

        [HttpGet("admin/{merchantId}")]
        public async Task<IActionResult> GetAdminVenues(int merchantId, [FromQuery] PaginationQuery pagination)
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            GetAdminVenuesQuery query = new GetAdminVenuesQuery()
            {
                MerchantId = merchantId,
                UserId = user.UserId,
                Pagination = pagination
            };

            var result = await _sender.Send(query);
            var mappedResult = result.Venues;

            return Ok(ApiResponseHelper.Success("Get admin venues successful", mappedResult));
        }

        [HttpPost("admin/{merchantId}")]
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
