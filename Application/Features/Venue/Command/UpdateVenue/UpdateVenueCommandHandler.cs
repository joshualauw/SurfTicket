﻿using MediatR;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Features.Merchant.Exceptions;
using SurfTicket.Application.Features.Venue.Exceptions;
using SurfTicket.Application.Services.Interface;
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
        private readonly ICurrentUserService _currentUserService;

        public UpdateVenueCommandHandler(IMerchantUserRepository merchantUserRepository,
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

        public async Task<UpdateVenueCommandResponse> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            var merchantUser = await _merchantUserRepository.GetMerchantUserAsync(request.MerchantId, _currentUserService.Payload.UserId);
            if (merchantUser == null)
            {
                throw new MerchantUserNotFoundException();
            }

            var permission = await _permissionAdminRepository.GetByCodeAsync(PermissionCode.VENUE);
            merchantUser.EnsureHasPermission(permission, PermissionAccess.UPDATE);

            var venue = await _venueRepository.GetAsync(request.VenueId);
            if (venue == null)
            {
                throw new VenueNotFoundException();
            }

            venue.Update(request.Name, request.Description);
            await _efUnitOfWork.SaveChangesAsync(_currentUserService.Payload.UserId);

            return new UpdateVenueCommandResponse();
        }
    }
}
