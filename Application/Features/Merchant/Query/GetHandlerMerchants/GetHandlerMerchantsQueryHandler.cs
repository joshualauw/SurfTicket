using MediatR;
using SurfTicket.Infrastructure.Repository.Interface;
using SurfTicket.Domain.Enums;

namespace SurfTicket.Application.Features.Merchant.Query.GetHandlerMerchants
{
    public class GetHandlerMerchantsQueryHandler : IRequestHandler<GetHandledMerchantsQuery, GetHandledMerchantsQueryResponse>
    {
        private readonly IMerchantRepository _merchantRepository;

        public GetHandlerMerchantsQueryHandler(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<GetHandledMerchantsQueryResponse> Handle(GetHandledMerchantsQuery request, CancellationToken cancellationToken)
        {
            var ownedMerchants = await _merchantRepository.GetMerchantsByRoleAsync(request.UserId, MerchantRole.OWNER);
            var collaboratedMerchants = await _merchantRepository.GetMerchantsByRoleAsync(request.UserId, MerchantRole.COLLABORATOR);

            return new GetHandledMerchantsQueryResponse()
            {
                OwnedMerchants = ownedMerchants,
                CollaboratedMerchants = collaboratedMerchants
            };
        }
    }
}
