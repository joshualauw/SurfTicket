using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Auth.Command.Me
{
    public class MeCommandHandler : IRequestHandler<MeCommand, MeCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;

        public MeCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<MeCommandResponse> Handle(MeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new BadRequestSurfException(SurfErrorCode.USER_NOT_FOUND, "User not found");
            }

            return new MeCommandResponse();
        }
    }
}
