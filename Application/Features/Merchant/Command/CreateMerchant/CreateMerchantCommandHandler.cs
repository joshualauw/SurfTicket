using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Models;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Application.Features.Merchant.Command.CreateMerchant
{
    public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, CreateMerchantCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly IEfUnitOfWork _efUnitOfWork;

        public CreateMerchantCommandHandler(UserManager<UserEntity> userManager, 
            ISubscriptionRepository subscriptionRepository, 
            IMerchantUserRepository merchantUserRepository, 
            IMerchantRepository merchantRepository, 
            IEfUnitOfWork efUnitOfWork)
        {
            _userManager = userManager;
            _subscriptionRepository = subscriptionRepository;
            _merchantUserRepository = merchantUserRepository;
            _merchantRepository = merchantRepository;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<CreateMerchantCommandResponse> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
        {         
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.USER_NOT_FOUND, "user not found");
            }

            var activeSubscription = await _subscriptionRepository.GetUserActiveSubscriptionAsync(user.Id);
            if (activeSubscription == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.READ_FAILED, "active subscription not found");
            }

            var ownedMerchants = await _merchantRepository.GetMerchantsByRoleAsync(user.Id, MerchantRole.OWNER);
            activeSubscription.EnsureCanCreateMerchant(ownedMerchants.Count);

            try
            {
                await _efUnitOfWork.BeginTransactionAsync();

                MerchantEntity merchant = new MerchantEntity()
                {
                    Name = request.Name,
                    Description = request.Description,
                };
                merchant.AddOwner(request.UserId);
                _merchantRepository.Create(merchant);

                await _efUnitOfWork.SaveChangesAsync();
                await _efUnitOfWork.CommitAsync();

                return new CreateMerchantCommandResponse()
                {
                    MerchantId = merchant.Id
                };
            }
            catch (Exception ex)
            {
                await _efUnitOfWork.RollbackAsync();
                throw new InternalSurfException(SurfErrorCode.INSERT_FAILED, "failed to create merchant", ex);
            }    
        }
    }
}
