using AutoMapper;
using MediatR;
using SurfTicket.Application.Features.Merchant.Exceptions;
using SurfTicket.Application.Features.Venue.Exceptions;
using SurfTicket.Application.Features.Venue.Query.GetAdminVenue.Dto;
using SurfTicket.Application.Services.Interface;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenue
{
    public class GetAdminVenueQueryHandler : IRequestHandler<GetAdminVenueQuery, GetAdminVenueQueryResponse>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly IPermissionAdminRepository _permissionAdminRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetAdminVenueQueryHandler(IMerchantUserRepository merchantUserRepository,
            IVenueRepository venueRepository,
            IPermissionAdminRepository permissionAdminRepository,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _merchantUserRepository = merchantUserRepository;
            _venueRepository = venueRepository;
            _permissionAdminRepository = permissionAdminRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<GetAdminVenueQueryResponse> Handle(GetAdminVenueQuery request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, _currentUserService.Payload.UserId);
            if (merchantUser == null)
            {
                throw new MerchantUserNotFoundException();
            }

            var permission = await _permissionAdminRepository.GetByCodeAsync(PermissionCode.VENUE);
            merchantUser.EnsureHasPermission(permission, PermissionAccess.VIEW);

            var venue = await _venueRepository.GetAsync(request.VenueId);
            if (venue == null)
            {
                throw new VenueNotFoundException();
            }

            var adminVenue = _mapper.Map<AdminVenueDetail>(venue);

            return new GetAdminVenueQueryResponse()
            {
                Detail = adminVenue
            };
        }
    }
}
