using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Enums;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Auth.Command.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UpdateProfileCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        public UpdateProfileCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UpdateProfileCommandResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByEmailAsync(request.OldEmail);

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

                throw new UnprocessableSurfException(SurfErrorCode.UPDATE_FAILED, errors);
            }

            return new UpdateProfileCommandResponse();
        }
    }
}
