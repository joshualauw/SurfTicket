using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Dto;
using SurfTicket.Infrastructure.Helpers;

namespace SurfTicket.Application.Features.Auth.Command.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, VerifyEmailCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        public VerifyEmailCommandHandler(UserManager<UserEntity> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<VerifyEmailCommandResponse> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            UserEntity? user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.USER_NOT_FOUND, "user not found");
            }

            if (user.VerifyCode == null || user.EmailConfirmed) 
            {
                throw new NotFoundSurfException(SurfErrorCode.READ_FAILED, "user already verified");
            }

            if (user.VerifyCode.ToLower() != request.VerifyCode.ToLower())
            {
                throw new BadRequestSurfException(SurfErrorCode.UNAUTHORIZED, "invalid verify code");
            }

            if (user.Email == null || user.UserName == null)
            {
                throw new BadRequestSurfException(SurfErrorCode.USER_NOT_FOUND, "Invalid credentials");
            }

            user.EmailConfirmed = true;
            user.VerifyCode = null;
            await _userManager.UpdateAsync(user);

            var token = UserJwtHelper.GenerateJwtToken(_configuration, new UserJwtPayload()
            {
                Email = user.Email,
                UserId = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
            });

            return new VerifyEmailCommandResponse()
            {
                Token = token
            };
        }
    }
}
