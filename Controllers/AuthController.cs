using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurfTicket.DTO.Auth;
using SurfTicket.Helpers;
using SurfTicket.Models;
using System.Net;

namespace SurfTicket.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public AuthController(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestBody body)
        {
            var newUser = new User
            {
                UserName = body.Email,
                Email = body.Email,
            };

            var result = await _userManager.CreateAsync(newUser, body.Password);
            if (result.Succeeded)
            {
                return Ok(ApiResponseHelper.Success<object>("Register: success"));
            }
            else
            {
                var errorCodes = result.Errors.Select(e => e.Code).ToList();
                var errorMessage = () =>
                {
                    if (errorCodes.Contains("DuplicateUserName"))
                    {
                        return "Email already in use";
                    }
                    else
                    {
                        return "Something unexcpected happen";
                    }
                };

                throw new HttpRequestException(errorMessage(), null, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestBody body)
        {
            var user = await _userManager.FindByEmailAsync(body.Email);
            if (user == null)
            {
                throw new HttpRequestException("Invalid credentials", null, HttpStatusCode.NotFound);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, body.Password);
            if (!isPasswordValid)
            {
                throw new HttpRequestException("Invalid credentials", null, HttpStatusCode.Unauthorized);
            }

            var token = UserJwtHelper.GenerateJwtToken(_configuration, user.Id);
            var userData = new
            {
                user.Id,
                user.UserName,
                user.Email,
            };

            return Ok(ApiResponseHelper.Success("Login: success", new
            {
                Token = token,
                User = userData
            }));
        }
    }
}
