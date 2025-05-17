using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Helpers;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Application.Features.Auth.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IConfiguration _configuration;

        public RegisterCommandHandler(UserManager<UserEntity> userManager, IConfiguration configuration, ISubscriptionRepository subscriptionRepository, IPlanRepository planRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _subscriptionRepository = subscriptionRepository;
            _planRepository = planRepository;
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
                        throw new BadRequestSurfException(SurfErrorCode.USER_EMAIL_ALREADY_USED, "email aready used");
                    }
                    else
                    {
                        errors += $"#{error.Code} - {error.Description}\n";
                    }
                }

                throw new UnprocessableSurfException(SurfErrorCode.INSERT_FAILED, errors);
            }

            var userData = await _userManager.FindByEmailAsync(request.Email);
            if (userData == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.USER_NOT_FOUND, "user not found");
            }

            var verifyCode = GenCodeHelper.GenerateCode(6);

            userData.VerifyCode = verifyCode;

            await _userManager.UpdateAsync(userData);

            try
            {
                EntityAudit audit = new EntityAudit() { CreatedBy = userData.Id};

                var basicPlan = await _planRepository.GetPlanByCodeAsync(PlanCode.BASIC);
                if (basicPlan != null)
                {
                    SubscriptionEntity subscription = new SubscriptionEntity()
                    {
                        PlanId = basicPlan.Id,
                        UserId = user.Id,
                        StartAt = DateTime.UtcNow,
                        IsActive = true

                    };
                    await _subscriptionRepository.CreateAsync(subscription, audit);
                }

                return new RegisterCommandResponse()
                {
                    Id = userData.Id,
                    Email = userData.Email,
                    Username = userData.UserName,
                    VerifyCode = verifyCode,
                };
            }
            catch (Exception ex)
            {
                throw new InternalSurfException(SurfErrorCode.INSERT_FAILED, "failed to insert user plan", ex);
            }
        }
    }
}
