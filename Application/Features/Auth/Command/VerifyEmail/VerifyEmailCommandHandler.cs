using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Features.Auth.Exceptions;
using SurfTicket.Application.Features.Merchant.Exceptions;
using SurfTicket.Application.Features.User.Exceptions;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Dto;
using SurfTicket.Infrastructure.Helpers;
using SurfTicket.Infrastructure.Repository.Interface;
using System.Security.Authentication;

namespace SurfTicket.Application.Features.Auth.Command.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, VerifyEmailCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ISubscriptionRepository _subscriptionRepository;
        public VerifyEmailCommandHandler(UserManager<UserEntity> userManager, IConfiguration configuration, ISubscriptionRepository subscriptionRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<VerifyEmailCommandResponse> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            UserEntity? user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (user.VerifyCode == null || user.EmailConfirmed) 
            {
                throw new EmailAlreadyVerifiedException();
            }

            if (user.VerifyCode.ToLower() != request.VerifyCode.ToLower())
            {
                throw new InvalidVerificationTokenException();
            }

            if (user.Email == null || user.UserName == null)
            {
                throw new InvalidCredentialsException(SurfErrorCode.USER_NOT_FOUND);
            }

            user.EmailConfirmed = true;
            user.VerifyCode = null;
            await _userManager.UpdateAsync(user);

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
                ActivePlan = activeSubscription.Plan.Code   
            });

            return new VerifyEmailCommandResponse()
            {
                Token = token
            };
        }
    }
}
