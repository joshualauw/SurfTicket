using MediatR;
using SurfTicket.Infrastructure.Repository.Interface;
using SurfTicket.Domain.Enums;
using SurfTicket.Application.Features.Merchant.Query.GetHandledMerchants.Dto;
using SurfTicket.Application.Services.Interface;

namespace SurfTicket.Application.Features.Merchant.Query.GetHandlerMerchants
{
    public class GetHandledMerchantsQueryHandler : IRequestHandler<GetHandledMerchantsQuery, GetHandledMerchantsQueryResponse>
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetHandledMerchantsQueryHandler(IMerchantRepository merchantRepository, ICurrentUserService currentUserService)
        {
            _merchantRepository = merchantRepository;
            _currentUserService = currentUserService;
        }

        public async Task<GetHandledMerchantsQueryResponse> Handle(GetHandledMerchantsQuery request, CancellationToken cancellationToken)
        {
            var ownedMerchants = await _merchantRepository.GetMerchantsByRoleAsync(_currentUserService.Payload.UserId, MerchantRole.OWNER);
            var collaboratedMerchants = await _merchantRepository.GetMerchantsByRoleAsync(_currentUserService.Payload.UserId, MerchantRole.COLLABORATOR);

            List<HandledMerchantItem> ownedMerchantsProjection = ownedMerchants
            .Select(om => new HandledMerchantItem()
            {
                Id = om.Id,
                Name = om.Name,
                LogoUrl = om.LogoUrl,
                LastVisited = om.CreatedAt,
            }).ToList();

            List<HandledMerchantItem> collaboratedMerchantsProjection = collaboratedMerchants
            .Select(cm => new HandledMerchantItem()
            {
                Id = cm.Id,
                Name = cm.Name,
                LogoUrl = cm.LogoUrl,
                LastVisited = cm.CreatedAt,
            }).ToList();

            return new GetHandledMerchantsQueryResponse()
            {
                OwnedMerchants = ownedMerchantsProjection,
                CollaboratedMerchants = collaboratedMerchantsProjection
            };      
        }
    }
}
