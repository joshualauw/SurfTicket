using MediatR;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Features.Venue.Query.GetAdminVenue.Dto;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Dto;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenue
{
    public class GetAdminVenueQueryHandler : IRequestHandler<GetAdminVenueQuery, GetAdminVenueQueryResponse>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly IPermissionAdminRepository _permissionAdminRepository;

        public GetAdminVenueQueryHandler(IMerchantUserRepository merchantUserRepository,
            IVenueRepository venueRepository,
            IPermissionAdminRepository permissionAdminRepository)
        {
            _merchantUserRepository = merchantUserRepository;
            _venueRepository = venueRepository;
            _permissionAdminRepository = permissionAdminRepository;
        }

        public async Task<GetAdminVenueQueryResponse> Handle(GetAdminVenueQuery request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, request.UserId);
            if (merchantUser == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.MERCHANT_USER_NOT_FOUND, "Merchant user not found");
            }

            var permission = await _permissionAdminRepository.GetByCodeAsync(PermissionCode.VENUE);
            merchantUser.EnsureHasPermission(permission, PermissionAccess.VIEW);

            var venue = await _venueRepository.GetAsync(request.VenueId);
            if (venue == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.VENUE_NOT_FOUND, "Venue not found");
            }

            AdminVenueDetail adminVenue = new AdminVenueDetail()
            {
                Id = venue.Id,
                Name = venue.Name,
                Description = venue.Description,
                LogoUrl = venue.LogoUrl
            };

            return new GetAdminVenueQueryResponse()
            {
                Detail = adminVenue
            };
        }
    }
}
