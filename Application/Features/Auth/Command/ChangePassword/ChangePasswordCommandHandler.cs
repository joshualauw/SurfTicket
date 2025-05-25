using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Auth.Command.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public ChangePasswordCommandHandler(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ChangePasswordCommandResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.USER_NOT_FOUND, "user not found");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.OldPassword);
            if (!passwordValid)
            {
                throw new BadRequestSurfException(SurfErrorCode.UNAUTHORIZED, "invalid old password");
            }

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                string errors = "";

                foreach (var error in result.Errors)
                {
                    errors += $"#{error.Code} - {error.Description}\n";
                }

                throw new UnprocessableSurfException(SurfErrorCode.UNAUTHORIZED, errors);
            }

            return new ChangePasswordCommandResponse();
        }
    }
}
