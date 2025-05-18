using MediatR;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Features.Venue.Query.GetAdminVenues.Dto;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Repository;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenues
{
    public class GetAdminVenuesQueryHandler : IRequestHandler<GetAdminVenuesQuery, GetAdminVenuesQueryResponse>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IPermissionAdminRepository _permissionAdminRepository;
        private readonly IVenueRepository _venueRepository;

        public GetAdminVenuesQueryHandler(IMerchantUserRepository merchantUserRepository, 
            IPermissionAdminRepository permissionAdminRepository, 
            IVenueRepository venueRepository)
        {
            _merchantUserRepository = merchantUserRepository;
            _permissionAdminRepository = permissionAdminRepository;
            _venueRepository = venueRepository;
        }

        public async Task<GetAdminVenuesQueryResponse> Handle(GetAdminVenuesQuery request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, request.UserId);
            if (merchantUser == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.MERCHANT_USER_NOT_FOUND, "Merchant user not found");
            }

            var permission = await _permissionAdminRepository.GetByCodeAsync(PermissionCode.VENUE);
            merchantUser.EnsureHasPermission(permission, PermissionAccess.VIEW);

            var venues = await _venueRepository.GetAdminVenues(request.MerchantId);
            var adminVenues = venues.Select(v => new AdminVenueItem()
            {
                Id = v.Id,
                Name = v.Name,
                LogoUrl = v.LogoUrl,
            }).ToList();

            return new GetAdminVenuesQueryResponse {
                Venues = adminVenues
            };
        }
    }
}
