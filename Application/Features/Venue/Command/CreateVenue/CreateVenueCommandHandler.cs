using MediatR;
using SurfTicket.Application.Exceptions;
using SurfTicket.Infrastructure.Repository.Interface;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Venue.Command.CreateVenue
{
    public class CreateVenueCommandHandler : IRequestHandler<CreateVenueCommand, CreateVenueCommandResponse>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly IEfUnitOfWork _efUnitOfWork;
        private readonly IPermissionAdminRepository _permissionAdminRepository;

        public CreateVenueCommandHandler(IMerchantUserRepository merchantUserRepository, 
            IVenueRepository venueRepository, 
            IEfUnitOfWork efUnitOfWork,
            IPermissionAdminRepository permissionAdminRepository)
        {
            _merchantUserRepository = merchantUserRepository;
            _venueRepository = venueRepository;
            _efUnitOfWork = efUnitOfWork;
            _permissionAdminRepository = permissionAdminRepository;
        }

        public async Task<CreateVenueCommandResponse> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, request.UserId);
            if (merchantUser == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.MERCHANT_USER_NOT_FOUND, "Merchant user not found");
            }

            var permission = await _permissionAdminRepository.GetByCodeAsync(PermissionCode.VENUE);
            merchantUser.EnsureHasPermission(permission, PermissionAccess.INSERT);

            try
            {
                VenueEntity venue = new VenueEntity()
                {
                    MerchantId = request.MerchantId,
                    Name = request.Name,
                    Description = request.Description,
                };

                _venueRepository.Create(venue);
                await _efUnitOfWork.SaveChangesAsync();

                return new CreateVenueCommandResponse
                {
                    VenueId = venue.Id,
                };
            }
            catch (Exception ex)
            {
                throw new InternalSurfException(SurfErrorCode.INSERT_FAILED, "failed to insert venue", ex);
            }
        }
    }
}
