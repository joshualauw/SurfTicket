﻿using MediatR;
using SurfTicket.Application.Exceptions;
using SurfTicket.Application.Features.Merchant.Exceptions;
using SurfTicket.Application.Features.Venue.Exceptions;
using SurfTicket.Application.Services.Interface;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.FileStorage;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Application.Features.Venue.Command.UploadVenueLogo
{
    public class UploadVenueLogoCommandHandler : IRequestHandler<UploadVenueLogoCommand, UploadVenueLogoCommandResponse>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IPermissionAdminRepository _permissionAdminRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IVenueRepository _venueRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IEfUnitOfWork _efUnitOfWork;
        public UploadVenueLogoCommandHandler(IMerchantUserRepository merchantUserRepository,
            IPermissionAdminRepository permissionAdminRepository,
            ICurrentUserService currentUserService,
            IVenueRepository venueRepository,
            IFileStorageService fileStorageService,
            IEfUnitOfWork efUnitOfWork)
        {
            _merchantUserRepository = merchantUserRepository;
            _venueRepository = venueRepository;
            _permissionAdminRepository = permissionAdminRepository;
            _currentUserService = currentUserService;
            _fileStorageService = fileStorageService;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<UploadVenueLogoCommandResponse> Handle(UploadVenueLogoCommand request, CancellationToken cancellationToken)
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

            if (venue.LogoUrl != null)
            {
                _fileStorageService.DeleteFile(venue.LogoUrl, "Venue");
            }

            var filePath = await _fileStorageService.SaveFileAsync(request.File, "Venue");
            venue.LogoUrl = filePath;

            await _efUnitOfWork.SaveChangesAsync(_currentUserService.Payload.UserId);

            return new UploadVenueLogoCommandResponse()
            {
                FilePath = filePath,
            };
        }
    }
}
