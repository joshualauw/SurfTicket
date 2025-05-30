using MediatR;
using SurfTicket.Infrastructure.Repository.Interface;
using SurfTicket.Domain.Enums;
using SurfTicket.Application.Features.Merchant.Query.GetHandledMerchants.Dto;
using SurfTicket.Application.Services.Interface;
using AutoMapper;

namespace SurfTicket.Application.Features.Merchant.Query.GetHandlerMerchants
{
    public class GetHandledMerchantsQueryHandler : IRequestHandler<GetHandledMerchantsQuery, GetHandledMerchantsQueryResponse>
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetHandledMerchantsQueryHandler(IMerchantRepository merchantRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _merchantRepository = merchantRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<GetHandledMerchantsQueryResponse> Handle(GetHandledMerchantsQuery request, CancellationToken cancellationToken)
        {
            var ownedMerchants = await _merchantRepository.GetMerchantsByRoleAsync(_currentUserService.Payload.UserId, MerchantRole.OWNER);
            var collaboratedMerchants = await _merchantRepository.GetMerchantsByRoleAsync(_currentUserService.Payload.UserId, MerchantRole.COLLABORATOR);

            var ownedMerchantsProjection = _mapper.Map<List<HandledMerchantItem>>(ownedMerchants);
            var collaboratedMerchantsProjection = _mapper.Map<List<HandledMerchantItem>>(collaboratedMerchants);

            return new GetHandledMerchantsQueryResponse()
            {
                OwnedMerchants = ownedMerchantsProjection,
                CollaboratedMerchants = collaboratedMerchantsProjection
            };      
        }
    }
}
