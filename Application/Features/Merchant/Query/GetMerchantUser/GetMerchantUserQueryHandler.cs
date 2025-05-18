using MediatR;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Features.Merchant.Query.GetMerchantUser.Dto;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Application.Features.Merchant.Query.GetMerchantUser
{
    public class GetMerchantUserQueryHandler : IRequestHandler<GetMerchantUserQuery, GetMerchantUserQueryResponse>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;
        public GetMerchantUserQueryHandler(IMerchantUserRepository merchantUserRepository)
        {
            _merchantUserRepository = merchantUserRepository;
        }

        public async Task<GetMerchantUserQueryResponse> Handle(GetMerchantUserQuery request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, request.UserId);
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
                Role = merchantUser.Role,
                Permissions = permissions,
            };
        }
    }
}
