using MediatR;
using Microsoft.AspNetCore.Identity;
using SurfTicket.Domain.Models;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Repository.Interface;
using SurfTicket.Application.Services.Interface;
using SurfTicket.Application.Features.Merchant.Exceptions;
using SurfTicket.Application.Features.User.Exceptions;

namespace SurfTicket.Application.Features.Merchant.Command.CreateMerchant
{
    public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, CreateMerchantCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEfUnitOfWork _efUnitOfWork;

        public CreateMerchantCommandHandler(UserManager<UserEntity> userManager, 
            ISubscriptionRepository subscriptionRepository, 
            IMerchantUserRepository merchantUserRepository, 
            IMerchantRepository merchantRepository, 
            ICurrentUserService currentUserService,
            IEfUnitOfWork efUnitOfWork)
        {
            _userManager = userManager;
            _subscriptionRepository = subscriptionRepository;
            _merchantUserRepository = merchantUserRepository;
            _merchantRepository = merchantRepository;
            _currentUserService = currentUserService;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<CreateMerchantCommandResponse> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
        {         
            var user = await _userManager.FindByIdAsync(_currentUserService.Payload.UserId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var activeSubscription = await _subscriptionRepository.GetUserActiveSubscriptionAsync(user.Id);
            if (activeSubscription == null)
            {
                throw new SubscriptionNotFoundException();
            }

            var ownedMerchants = await _merchantRepository.GetMerchantsByRoleAsync(user.Id, MerchantRole.OWNER);
            activeSubscription.EnsureCanCreateMerchant(ownedMerchants.Count);

            MerchantEntity merchant = MerchantEntity.Create(request.Name, request.Description, user.Id);
            _merchantRepository.Create(merchant);

            await _efUnitOfWork.SaveChangesAsync(_currentUserService.Payload.UserId);

            return new CreateMerchantCommandResponse()
            {
                MerchantId = merchant.Id
            };              
        }
    }
}
