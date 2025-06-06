using MediatR;
using SurfTicket.Infrastructure.Common;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Features.Venue.Query.GetAdminVenues.Dto;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Repository.Interface;
using SurfTicket.Application.Services.Interface;
using AutoMapper;
using SurfTicket.Application.Features.Merchant.Exceptions;

namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenues
{
    public class GetAdminVenuesQueryHandler : IRequestHandler<GetAdminVenuesQuery, GetAdminVenuesQueryResponse>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IPermissionAdminRepository _permissionAdminRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetAdminVenuesQueryHandler(IMerchantUserRepository merchantUserRepository, 
            IPermissionAdminRepository permissionAdminRepository, 
            IVenueRepository venueRepository,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _merchantUserRepository = merchantUserRepository;
            _permissionAdminRepository = permissionAdminRepository;
            _venueRepository = venueRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<GetAdminVenuesQueryResponse> Handle(GetAdminVenuesQuery request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, _currentUserService.Payload.UserId);
            if (merchantUser == null)
            {
                throw new MerchantUserNotFoundException();
            }

            var permission = await _permissionAdminRepository.GetByCodeAsync(PermissionCode.VENUE);
            merchantUser.EnsureHasPermission(permission, PermissionAccess.VIEW);    

            var venues = await _venueRepository.GetAdminVenues(request.MerchantId, request.Filter);

            var adminVenues = _mapper.Map<List<AdminVenueItem>>(venues.Items);

            return new GetAdminVenuesQueryResponse()
            {
                Venues = new PagedResult<AdminVenueItem>()
                {
                    Items = adminVenues,
                    TotalItems = venues.TotalItems,
                    Page = venues.Page,
                    Size = venues.Size
                }
            };
        }
    }
}
