using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Services.Interface;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Auth.Command.Me
{
    public class MeCommandHandler : IRequestHandler<MeCommand, MeCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public MeCommandHandler(UserManager<UserEntity> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<MeCommandResponse> Handle(MeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_currentUserService.Payload.UserId);
            if (user == null)
            {
                throw new BadRequestSurfException(SurfErrorCode.USER_NOT_FOUND, "User not found");
            }

            return new MeCommandResponse()
            {
                User = _currentUserService.Payload
            };
        }
    }
}
