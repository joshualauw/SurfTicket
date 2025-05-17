using MediatR;
using SurfTicket.Infrastructure.Repository.Interface;
using SurfTicket.Domain.Enums;
using SurfTicket.Application.Features.Merchant.Query.GetHandledMerchants.Dto;
using SurfTicket.Application.Exceptions;

namespace SurfTicket.Application.Features.Merchant.Query.GetHandlerMerchants
{
    public class GetHandledMerchantsQueryHandler : IRequestHandler<GetHandledMerchantsQuery, GetHandledMerchantsQueryResponse>
    {
        private readonly IMerchantRepository _merchantRepository;

        public GetHandledMerchantsQueryHandler(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<GetHandledMerchantsQueryResponse> Handle(GetHandledMerchantsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var ownedMerchants = await _merchantRepository.GetMerchantsByRoleAsync(request.UserId, MerchantRole.OWNER);
                var collaboratedMerchants = await _merchantRepository.GetMerchantsByRoleAsync(request.UserId, MerchantRole.COLLABORATOR);

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
            catch (Exception ex)
            {
                throw new InternalSurfException(SurfErrorCode.READ_FAILED, "failed to get handled merchants", ex);
            }
        }
    }
}
