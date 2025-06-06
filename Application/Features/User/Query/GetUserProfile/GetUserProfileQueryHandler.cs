using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Features.User.Exceptions;
using SurfTicket.Application.Services.Interface;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.User.Query.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileQueryResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;

        public GetUserProfileQueryHandler(UserManager<UserEntity> userManager, IConfiguration configuration, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _currentUserService = currentUserService;
        }

        public async Task<GetUserProfileQueryResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_currentUserService.Payload.UserId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return new GetUserProfileQueryResponse()
            {
                Email = user.Email,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }
    }
}
