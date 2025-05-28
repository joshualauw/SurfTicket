using MediatR;
using SurfTicket.Application.Exceptions;
using SurfTicket.Infrastructure.Repository.Interface;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;
using SurfTicket.Application.Services.Interface;

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
                throw new NotFoundSurfException(SurfErrorCode.MERCHANT_USER_NOT_FOUND, "Merchant user not found");
            }

            var permission = await _permissionAdminRepository.GetByCodeAsync(PermissionCode.VENUE);
            merchantUser.EnsureHasPermission(permission, PermissionAccess.INSERT);

            try
            {
                var venue = VenueEntity.Create(request.MerchantId, request.Name, request.Description);
                _venueRepository.Create(venue);

                await _efUnitOfWork.SaveChangesAsync();

                return new CreateVenueCommandResponse
                {
                    VenueId = venue.Id,
                };
            }
            catch (Exception ex)
            {
                throw new InternalSurfException(SurfErrorCode.VENUE_CREATE_FAILED, "failed to create venue", ex);
            }
        }
    }
}
