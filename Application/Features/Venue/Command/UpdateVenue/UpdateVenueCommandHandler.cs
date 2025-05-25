using MediatR;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Application.Features.Venue.Command.UpdateVenue
{
    public class UpdateVenueCommandHandler : IRequestHandler<UpdateVenueCommand, UpdateVenueCommandResponse>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly IEfUnitOfWork _efUnitOfWork;
        private readonly IPermissionAdminRepository _permissionAdminRepository;

        public UpdateVenueCommandHandler(IMerchantUserRepository merchantUserRepository,
            IVenueRepository venueRepository,
            IEfUnitOfWork efUnitOfWork,
            IPermissionAdminRepository permissionAdminRepository)
        {
            _merchantUserRepository = merchantUserRepository;
            _venueRepository = venueRepository;
            _efUnitOfWork = efUnitOfWork;
            _permissionAdminRepository = permissionAdminRepository;
        }

        public async Task<UpdateVenueCommandResponse> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, request.UserId);
            if (merchantUser == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.MERCHANT_USER_NOT_FOUND, "Merchant user not found");
            }

            var permission = await _permissionAdminRepository.GetByCodeAsync(PermissionCode.VENUE);
            merchantUser.EnsureHasPermission(permission, PermissionAccess.UPDATE);

            var venue = await _venueRepository.GetAsync(request.VenueId);
            if (venue == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.VENUE_NOT_FOUND, "Venue not found");
            }

            venue.Update(request.Name, request.Description);
            await _efUnitOfWork.SaveChangesAsync();

            return new UpdateVenueCommandResponse();
        }
    }
}
