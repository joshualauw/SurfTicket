using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Features.Auth.Exceptions;
using SurfTicket.Application.Features.User.Exceptions;
using SurfTicket.Application.Services.Interface;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Auth.Command.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _currentUserService = currentUserService;
        }

        public async Task<ChangePasswordCommandResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_currentUserService.Payload.UserId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.OldPassword);
            if (!passwordValid)
            {
                throw new SurfException(SurfErrorCode.UNAUTHORIZED, "invalid old password", 400);
            }

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                string errors = "";

                foreach (var error in result.Errors)
                {
                    errors += $"#{error.Code} - {error.Description}\n";
                }

                throw new SurfException(SurfErrorCode.UNAUTHORIZED, errors, 401);
            }

            return new ChangePasswordCommandResponse();
        }
    }
}
