using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Models;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Repository.Interface;
using SurfTicket.Infrastructure.Data;

namespace SurfTicket.Application.Features.Merchant.Command.CreateMerchant
{
    public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, CreateMerchantCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly AppDbContext _dbContext;

        public CreateMerchantCommandHandler(UserManager<UserEntity> userManager, 
            ISubscriptionRepository subscriptionRepository, 
            IMerchantUserRepository merchantUserRepository, 
            IMerchantRepository merchantRepository, 
            AppDbContext dbContext)
        {
            _userManager = userManager;
            _subscriptionRepository = subscriptionRepository;
            _merchantUserRepository = merchantUserRepository;
            _merchantRepository = merchantRepository;
            _dbContext = dbContext;
        }

        public async Task<CreateMerchantCommandResponse> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
        {
            UserEntity? user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.USER_NOT_FOUND, "user not found");
            }

            SubscriptionEntity? activeSubscription = await _subscriptionRepository.GetUserActiveSubscriptionAsync(user.Id);
            if (activeSubscription == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.READ_FAILED, "active subscription not found");
            }

            int merchantOwned = await _merchantUserRepository.GetUserMerchantCountAsync(user.Id);

            if (merchantOwned >= activeSubscription.Plan.Features.MaxOwnedMerchant)
            {
                throw new BadRequestSurfException(SurfErrorCode.MERCHANT_EXCEED, "maximum number of merchant created");
            }

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    MerchantEntity merchant = new MerchantEntity()
                    {
                        Name = request.Name,
                        Description = request.Description
                    };
                    await _merchantRepository.CreateAsync(merchant);

                    MerchantUserEntity owner = new MerchantUserEntity()
                    {
                        MerchantId = merchant.Id,
                        UserId = user.Id,
                        Role = MerchantRole.OWNER
                    };
                    await _merchantUserRepository.CreateAsync(owner);

                    await _merchantUserRepository.AssignOwnerPermissionAsync(owner.Id);

                    await transaction.CommitAsync();

                    return new CreateMerchantCommandResponse()
                    {
                        MerchantId = merchant.Id
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new InternalSurfException(SurfErrorCode.INSERT_FAILED, "failed to create merchant");
                }
            }  
        }
    }
}
