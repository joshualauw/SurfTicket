using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Features.User.Exceptions;
using SurfTicket.Application.Services.Interface;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.User.Command.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UpdateProfileCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ICurrentUserService _currentUserService;
        public UpdateProfileCommandHandler(UserManager<UserEntity> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<UpdateProfileCommandResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(_currentUserService.Payload.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
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

                throw new SurfException(SurfErrorCode.UNAUTHORIZED, errors, 401);
            }

            return new UpdateProfileCommandResponse();
        }
    }
}
