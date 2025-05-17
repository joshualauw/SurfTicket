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

        public CreateVenueCommandHandler(IMerchantUserRepository merchantUserRepository, IVenueRepository venueRepository)
        {
            _merchantUserRepository = merchantUserRepository;
            _venueRepository = venueRepository;
        }

        public async Task<CreateVenueCommandResponse> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, request.UserId);
            if (merchantUser == null)
            {
                throw new NotFoundSurfException(SurfErrorCode.MERCHANT_VIOLATE_PERMISSION, "Merchant user not found");
            }

            var hasPermission = await _merchantUserRepository.HasPermissionAsync(merchantUser, PermissionCode.VENUE, PermissionAccess.INSERT);
            if (!hasPermission)
            {
                throw new BadRequestSurfException(SurfErrorCode.MERCHANT_VIOLATE_PERMISSION, "Merchant user does not have access");
            }

            try
            {
                EntityAudit audit = new EntityAudit() { CreatedBy = request.UserId };

                VenueEntity venue = new VenueEntity()
                {
                    MerchantId = request.MerchantId,
                    Name = request.Name,
                    Description = request.Description,
                };

                await _venueRepository.CreateAsync(venue, audit);

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
