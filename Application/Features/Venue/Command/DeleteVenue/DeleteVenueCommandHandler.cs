using MediatR;
using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Application.Features.Venue.Command.DeleteVenue
{
    public class DeleteVenueCommandHandler : IRequestHandler<DeleteVenueCommand, DeleteVenueCommandResponse>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly IEfUnitOfWork _efUnitOfWork;
        private readonly IPermissionAdminRepository _permissionAdminRepository;

        public DeleteVenueCommandHandler(IMerchantUserRepository merchantUserRepository,
            IVenueRepository venueRepository,
            IEfUnitOfWork efUnitOfWork,
            IPermissionAdminRepository permissionAdminRepository)
        {
            _merchantUserRepository = merchantUserRepository;
            _venueRepository = venueRepository;
            _efUnitOfWork = efUnitOfWork;
            _permissionAdminRepository = permissionAdminRepository;
        }

        public async Task<DeleteVenueCommandResponse> Handle(DeleteVenueCommand request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, request.UserId);
            if (merchantUser == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.MERCHANT_USER_NOT_FOUND, "Merchant user not found");
            }

            var permission = await _permissionAdminRepository.GetByCodeAsync(PermissionCode.VENUE);
            merchantUser.EnsureHasPermission(permission, PermissionAccess.DELETE);

            var venue = await _venueRepository.GetAsync(request.VenueId);
            if (venue == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.VENUE_NOT_FOUND, "Venue not found");
            }

            _venueRepository.Remove(venue);
            await _efUnitOfWork.SaveChangesAsync();

            return new DeleteVenueCommandResponse();
        }
    }
}
