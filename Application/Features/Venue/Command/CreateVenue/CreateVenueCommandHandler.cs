using MediatR;
using SurfTicket.Application.Exceptions;
using SurfTicket.Infrastructure.Repository.Interface;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;
using SurfTicket.Application.Services.Interface;
using SurfTicket.Application.Features.Merchant.Exceptions;
using SurfTicket.Application.Features.Venue.Exceptions;

namespace SurfTicket.Application.Features.Venue.Command.CreateVenue
{
    public class CreateVenueCommandHandler : IRequestHandler<CreateVenueCommand, CreateVenueCommandResponse>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly IEfUnitOfWork _efUnitOfWork;
        private readonly IPermissionAdminRepository _permissionAdminRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateVenueCommandHandler(IMerchantUserRepository merchantUserRepository, 
            IVenueRepository venueRepository, 
            IEfUnitOfWork efUnitOfWork,
            IPermissionAdminRepository permissionAdminRepository,
            ICurrentUserService currentUserService)
        {
            _merchantUserRepository = merchantUserRepository;
            _venueRepository = venueRepository;
            _efUnitOfWork = efUnitOfWork;
            _permissionAdminRepository = permissionAdminRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CreateVenueCommandResponse> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, _currentUserService.Payload.UserId);
            if (merchantUser == null)
            {
                throw new MerchantUserNotFoundException();
            }

            var permission = await _permissionAdminRepository.GetByCodeAsync(PermissionCode.VENUE);
            merchantUser.EnsureHasPermission(permission, PermissionAccess.INSERT);

            var venue = VenueEntity.Create(request.MerchantId, request.Name, request.Description);
            _venueRepository.Create(venue);

            await _efUnitOfWork.SaveChangesAsync(_currentUserService.Payload.UserId);

            return new CreateVenueCommandResponse
            {
                VenueId = venue.Id,
            };
        }
    }
}
