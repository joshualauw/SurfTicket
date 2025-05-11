using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurfTicket.Application.Features.User.Command.UpdateProfile;
using SurfTicket.Application.Features.User.Query;
using SurfTicket.Infrastructure.Dto;
using SurfTicket.Infrastructure.Helpers;
using SurfTicket.Presentation.Dto.User;
using SurfTicket.Presentation.Helpers;

namespace SurfTicket.Presentation.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISender _sender;
        public UserController(IConfiguration configuration, ISender sender)
        {
            _configuration = configuration;
            _sender = sender;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            GetUserProfileQuery query = new GetUserProfileQuery()
            {
                UserId = user.UserId
            };

            var result = await _sender.Send(query);

            return Ok(ApiResponseHelper.Success("Get user profile succesful", result));
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileBody body)
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            UpdateProfileCommand command = new UpdateProfileCommand()
            {
                NewEmail = body.Email,
                OldEmail = user.Email,
                FirstName = body.FirstName,
                LastName = body.LastName,
            };

            var result = await _sender.Send(command);

            return Ok(ApiResponseHelper.Success("Update profile successful", result));
        }
    }
}
