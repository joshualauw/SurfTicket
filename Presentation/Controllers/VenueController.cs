using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurfTicket.Infrastructure.Common;
using SurfTicket.Application.Features.Venue.Command.CreateVenue;
using SurfTicket.Application.Features.Venue.Query.GetAdminVenues;
using SurfTicket.Presentation.Dto.Venue;
using SurfTicket.Presentation.Helpers;
using SurfTicket.Application.Features.Venue.Command.UpdateVenue;
using SurfTicket.Application.Features.Venue.Query.GetAdminVenue;
using SurfTicket.Application.Features.Venue.Command.DeleteVenue;
using SurfTicket.Application.Features.Venue.Command.UploadVenueLogo;

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
            GetAdminVenuesQuery query = new GetAdminVenuesQuery()
            {
                MerchantId = merchantId,
                Filter = filter
            };

            var result = await _sender.Send(query);
            var mappedResult = result.Venues;

            return Ok(ApiResponseHelper.Success("Get admin venues successful", mappedResult));
        }

        [HttpPost("admin/{merchantId}")]
        public async Task<IActionResult> CreateVenue(int merchantId, [FromBody] CreateVenueBody body)
        {
            CreateVenueCommand command = new CreateVenueCommand()
            {
               MerchantId = merchantId,
               Name = body.Name,
               Description = body.Description,
            };

            var result = await _sender.Send(command);

            return Ok(ApiResponseHelper.Success("Create venue succesful", result));
        }

        [HttpGet("admin/{merchantId}/{venueId}")]
        public async Task<IActionResult> UpdateVenue(int merchantId, int venueId)
        {
            GetAdminVenueQuery query = new GetAdminVenueQuery()
            {
                MerchantId = merchantId,
                VenueId = venueId,
            };

            var result = await _sender.Send(query);
            var mappedResult = result.Detail;

            return Ok(ApiResponseHelper.Success("Get admin venue succesful", mappedResult));
        }

        [HttpPost("admin/{merchantId}/{venueId}/upload")]
        public async Task<IActionResult> UploadVenueLogo(int merchantId, int venueId, [FromForm] UploadVenueLogoBody body)
        {
            UploadVenueLogoCommand command = new UploadVenueLogoCommand()
            {
                MerchantId = merchantId,
                VenueId = venueId,
                File = body.Logo
            };

            var result = await _sender.Send(command);
            return Ok(ApiResponseHelper.Success("Upload venue logo succesful", result));
        }

        [HttpPut("admin/{merchantId}/{venueId}")]
        public async Task<IActionResult> UpdateVenue(int merchantId, int venueId, [FromBody] UpdateVenueBody body)
        {
            UpdateVenueCommand command = new UpdateVenueCommand()
            {
                MerchantId = merchantId,
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
            DeleteVenueCommand command = new DeleteVenueCommand()
            {
                MerchantId = merchantId,
                VenueId = venueId,
            };

            var result = await _sender.Send(command);

            return Ok(ApiResponseHelper.Success("Delete venue succesful", result));
        }
    }
}
