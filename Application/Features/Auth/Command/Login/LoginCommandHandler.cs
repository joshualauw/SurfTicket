using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Enums;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Helpers;

namespace SurfTicket.Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new BadRequestSurfException(SurfErrorCode.USER_NOT_FOUND, "Invalid credentials", "LoginCommand");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                throw new BadRequestSurfException(SurfErrorCode.USER_WRONG_PASSWORD, "Invalid credentials", "LoginCommand");
            }

            var token = UserJwtHelper.GenerateJwtToken(_configuration, user.Id, user.Email ?? "", user.UserName ?? "");

            return new LoginCommandResponse
            {
                Token = token,
                User = new LoginCommandUser()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email
                }
            };

        }
    }
}
