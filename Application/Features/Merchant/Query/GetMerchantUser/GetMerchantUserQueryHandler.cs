using AutoMapper;
using MediatR;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Features.Merchant.Query.GetMerchantUser.Dto;
using SurfTicket.Application.Services.Interface;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Application.Features.Merchant.Query.GetMerchantUser
{
    public class GetMerchantUserQueryHandler : IRequestHandler<GetMerchantUserQuery, GetMerchantUserQueryResponse>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public GetMerchantUserQueryHandler(IMerchantUserRepository merchantUserRepository, 
            IMerchantRepository merchantRepository, 
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _merchantUserRepository = merchantUserRepository;
            _merchantRepository = merchantRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<GetMerchantUserQueryResponse> Handle(GetMerchantUserQuery request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, _currentUserService.Payload.UserId);
            if (merchantUser == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.MERCHANT_USER_NOT_FOUND, "Merchant user not found");
            }

            var permissionMenus = await _merchantUserRepository.GetMerchantUserPermissionsAsync(merchantUser.Id);

            var permissions = _mapper.Map<List<PermissionMenuItem>>(permissionMenus);

            return new GetMerchantUserQueryResponse()
            { 
                Id = merchantUser.Id,
                MerchantId = request.MerchantId,
                Role = merchantUser.Role,
                Permissions = permissions,
            };
        }
    }
}
