using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.User.Query.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileQueryResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;

        public GetUserProfileQueryHandler(UserManager<UserEntity> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<GetUserProfileQueryResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new BadRequestSurfException(SurfErrorCode.USER_NOT_FOUND, "user not found");
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
