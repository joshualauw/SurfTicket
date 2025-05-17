using MediatR;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenues
{
    public class GetAdminVenusQueryHandler : IRequestHandler<GetAdminVenuesQuery, GetAdminVenusQueryResponse>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;

        public GetAdminVenusQueryHandler(IMerchantUserRepository merchantUserRepository)
        {
            _merchantUserRepository = merchantUserRepository;
        }

        public async Task<GetAdminVenusQueryResponse> Handle(GetAdminVenuesQuery request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, request.UserId);
            if (merchantUser == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.MERCHANT_VIOLATE_PERMISSION, "Merchant user not found");
            }

            var hasPermission = await _merchantUserRepository.HasPermissionAsync(merchantUser, PermissionCode.VENUE, PermissionAccess.VIEW);
            if (!hasPermission)
            {
                throw new BadRequestSurfException(SurfErrorCode.MERCHANT_VIOLATE_PERMISSION, "Merchant user does not have access");
            }

            return new GetAdminVenusQueryResponse { };
        }
    }
}
