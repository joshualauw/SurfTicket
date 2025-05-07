using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SurfTicket.Application.Enums;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Auth.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public RegisterCommandHandler(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            User user = new User()
            {
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                string errors = "";

                foreach (var error in result.Errors)
                {
                    if (error.Code.Equals("DuplicateUserName"))
                    {
                        throw new BadRequestSurfException(SurfErrorCode.USER_EMAIL_ALREADY_USED, "email aready used", "RegisterCommand");
                    }
                    else
                    {
                        errors += $"#{error.Code} - {error.Description}\n";
                    }
                }

                throw new SurfException(SurfErrorCode.INSERT_FAILED, errors, "RegisterCommand");
            }

            var userData = await _userManager.FindByEmailAsync(request.Email);
            if (userData == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.USER_NOT_FOUND, "user not found", "RegisterCommannd");
            }

            return new RegisterCommandResponse()
            {
                Id = userData.Id,
                Email = userData.Email,
                Username = userData.UserName
            };
        }
    }
}
