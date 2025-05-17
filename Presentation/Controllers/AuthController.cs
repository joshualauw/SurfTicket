using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurfTicket.Application.Features.Auth.Command.ChangePassword;
using SurfTicket.Application.Features.Auth.Command.Login;
using SurfTicket.Application.Features.Auth.Command.Me;
using SurfTicket.Application.Features.Auth.Command.Register;
using SurfTicket.Application.Features.Auth.Command.VerifyEmail;
using SurfTicket.Infrastructure.Dto;
using SurfTicket.Infrastructure.Helpers;
using SurfTicket.Presentation.Dto.Auth;
using SurfTicket.Presentation.Helpers;


namespace SurfTicket.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISender _sender;
        public AuthController(IConfiguration configuration, ISender sender)
        {
            _configuration = configuration;
            _sender = sender;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            MeCommand command = new MeCommand()
            {
                UserId = user.UserId,
            };

            await _sender.Send(command);

            return Ok(ApiResponseHelper.Success("Get me successful", user));
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailBody body)
        {
            VerifyEmailCommand command = new VerifyEmailCommand()
            {
                VerifyCode = body.VerifyCode,
                Email = body.Email,
            };

            var result = await _sender.Send(command);

            return Ok(ApiResponseHelper.Success("Verify email successful", result));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginBody body)
        {
            LoginCommand command = new LoginCommand()
            {
                Email = body.Email,
                Password = body.Password,
            };

            var result = await _sender.Send(command);              

            return Ok(ApiResponseHelper.Success("Login successful", result));           
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterBody body)
        {
            RegisterCommand command = new RegisterCommand()
            {
                Email = body.Email,
                Password = body.Password,
            };

            var result = await _sender.Send(command);

            return Ok(ApiResponseHelper.Success("Register successful", result));
        }

        [HttpPut("password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordBody body)
        {
            UserJwtPayload user = UserJwtHelper.GetJwtUser(HttpContext);

            ChangePasswordCommand command = new ChangePasswordCommand()
            {
                UserId = user.UserId,
                OldPassword = body.OldPassword,
                NewPassword = body.Password,
            };

            var result = await _sender.Send(command);

            return Ok(ApiResponseHelper.Success("Change password succesful", result));
        }
    }
}
