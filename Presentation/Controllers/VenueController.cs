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
using SurfTicket.Application.Features.Venue.Command.UpdateVenue;
using SurfTicket.Application.Features.Venue.Query.GetAdminVenue;
using SurfTicket.Application.Features.Venue.Command.DeleteVenue;

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
        public async Task<IActionResult> GetAdminVenues(int merchantId, [FromQuery] FilterQuery filter)
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            GetAdminVenuesQuery query = new GetAdminVenuesQuery()
            {
                MerchantId = merchantId,
                UserId = user.UserId,
                Filter = filter
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

        [HttpGet("admin/{merchantId}/{venueId}")]
        public async Task<IActionResult> UpdateVenue(int merchantId, int venueId)
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            GetAdminVenueQuery query = new GetAdminVenueQuery()
            {
                MerchantId = merchantId,
                UserId = user.UserId,
                VenueId = venueId,
            };

            var result = await _sender.Send(query);
            var mappedResult = result.Detail;

            return Ok(ApiResponseHelper.Success("Get admin venue succesful", mappedResult));
        }

        [HttpPut("admin/{merchantId}/{venueId}")]
        public async Task<IActionResult> UpdateVenue(int merchantId, int venueId, [FromBody] UpdateVenueBody body)
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            UpdateVenueCommand command = new UpdateVenueCommand()
            {
                MerchantId = merchantId,
                UserId = user.UserId,
                VenueId = venueId,
                Name = body.Name,
                Description = body.Description,
            };

            var result = await _sender.Send(command);

            return Ok(ApiResponseHelper.Success("Update venue succesful", result));
        }

        [HttpDelete("admin/{merchantId}/{venueId}")]
        public async Task<IActionResult> DeleteVenue(int merchantId, int venueId)
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            DeleteVenueCommand command = new DeleteVenueCommand()
            {
                MerchantId = merchantId,
                UserId = user.UserId,
                VenueId = venueId,
            };

            var result = await _sender.Send(command);

            return Ok(ApiResponseHelper.Success("Delete venue succesful", result));
        }
    }
}
