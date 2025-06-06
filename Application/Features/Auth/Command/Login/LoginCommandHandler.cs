using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Helpers;
using SurfTicket.Infrastructure.Dto;
using SurfTicket.Infrastructure.Repository.Interface;
using SurfTicket.Application.Features.Auth.Exceptions;
using SurfTicket.Application.Features.Merchant.Exceptions;

namespace SurfTicket.Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public LoginCommandHandler(UserManager<UserEntity> userManager, 
            SignInManager<UserEntity> signInManager, 
            IConfiguration configuration, 
            ISubscriptionRepository subscriptionRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new InvalidCredentialsException(SurfErrorCode.USER_NOT_FOUND);
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
            {
                if (result.IsNotAllowed && !user.EmailConfirmed)
                {
                    throw new EmailNotVerifiedException();
                }
                else
                {
                    throw new InvalidCredentialsException(SurfErrorCode.USER_WRONG_PASSWORD);
                }
            }

            if (user.Email == null || user.UserName == null)
            {
                throw new InvalidCredentialsException(SurfErrorCode.USER_NOT_FOUND);
            }

            var activeSubscription = await _subscriptionRepository.GetUserActiveSubscriptionAsync(user.Id);
            if (activeSubscription == null)
            {
                throw new SubscriptionNotFoundException();
            }

            var token = UserJwtHelper.GenerateJwtToken(_configuration, new UserJwtPayload()
            {
                Email = user.Email,
                UserId = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ActivePlan = activeSubscription.Plan.Code,
            });

            return new LoginCommandResponse
            {
                Token = token,
            };

        }
    }
}
