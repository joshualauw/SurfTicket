using AutoMapper;
using SurfTicket.Application.Features.Merchant.Query.GetHandledMerchants.Dto;
using SurfTicket.Application.Features.Merchant.Query.GetMerchantUser.Dto;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Merchant.Mapper
{
    public class MerchantProfile : Profile
    {
        public MerchantProfile()
        {
            CreateMap<MerchantEntity, HandledMerchantItem>()
                .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom<MerchantLogoResolver>())
                .ForMember(dest => dest.LastVisited, opt => opt.MapFrom(m => m.CreatedAt));

            CreateMap<PermissionMenuEntity, PermissionMenuItem>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(pm => pm.PermissionAdmin.Code));
        }
    }
}
