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
        public GetMerchantUserQueryHandler(IMerchantUserRepository merchantUserRepository, IMerchantRepository merchantRepository, ICurrentUserService currentUserService)
        {
            _merchantUserRepository = merchantUserRepository;
            _merchantRepository = merchantRepository;
            _currentUserService = currentUserService;
        }

        public async Task<GetMerchantUserQueryResponse> Handle(GetMerchantUserQuery request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, _currentUserService.Payload.UserId);
            if (merchantUser == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.MERCHANT_USER_NOT_FOUND, "Merchant user not found");
            }

            var permissionMenus = await _merchantUserRepository.GetMerchantUserPermissionsAsync(merchantUser.Id);

            List<PermissionMenuItem> permissions = permissionMenus.Select(p => new PermissionMenuItem()
            {
                Id = p.Id,
                Code = p.PermissionAdmin.Code,
                Access = p.Access,
            }).ToList();

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
