using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.User.Command.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UpdateProfileCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        public UpdateProfileCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UpdateProfileCommandResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.OldEmail);

            if (user == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.USER_NOT_FOUND, "user not found");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.NewEmail;
            user.UserName = request.NewEmail;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                string errors = "";

                foreach (var error in result.Errors)
                {
                    errors += $"#{error.Code} - {error.Description}\n";
                }

                throw new UnprocessableSurfException(SurfErrorCode.UNAUTHORIZED, errors);
            }

            return new UpdateProfileCommandResponse();
        }
    }
}
