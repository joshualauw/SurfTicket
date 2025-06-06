using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Features.Auth.Exceptions;
using SurfTicket.Application.Features.User.Exceptions;
using SurfTicket.Application.Features.Merchant.Exceptions;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Helpers;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Application.Features.Auth.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IEfUnitOfWork _efUnitOfWork;

        public RegisterCommandHandler(
            UserManager<UserEntity> userManager, 
            IConfiguration configuration, 
            ISubscriptionRepository subscriptionRepository, 
            IPlanRepository planRepository,
            IEfUnitOfWork efUnitOfWork)
        {
            _userManager = userManager;
            _configuration = configuration;
            _subscriptionRepository = subscriptionRepository;
            _planRepository = planRepository;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = new UserEntity()
            {
                Email = request.Email,
                UserName = request.Email,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                string errors = "";

                foreach (var error in result.Errors)
                {
                    if (error.Code.Equals("DuplicateUserName"))
                    {
                        throw new EmailAlreadyUsedException();
                    }
                    else
                    {
                        errors += $"#{error.Code} - {error.Description}\n";
                    }
                }

                throw new SurfException(SurfErrorCode.UNAUTHORIZED, errors, 400);
            }

            var userData = await _userManager.FindByEmailAsync(request.Email);
            if (userData == null)
            {
                throw new UserNotFoundException();
            }

            var verifyCode = GenCodeHelper.GenerateCode(6);
            userData.VerifyCode = verifyCode;
            await _userManager.UpdateAsync(userData);

            var basicPlan = await _planRepository.GetPlanByCodeAsync(PlanCode.BASIC);
            if (basicPlan == null)
            {
                throw new PlanNotFoundException();
            }

            SubscriptionEntity subscription = SubscriptionEntity.Create(basicPlan.Id, user.Id);
            _subscriptionRepository.Create(subscription);

            await _efUnitOfWork.SaveChangesAsync();    

            return new RegisterCommandResponse()
            {
                Id = userData.Id,
                Email = userData.Email,
                Username = userData.UserName,
                VerifyCode = verifyCode,
            };
        }
    }
}
