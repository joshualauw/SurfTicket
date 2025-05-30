using AutoMapper;
using SurfTicket.Application.Features.Venue.Query.GetAdminVenue.Dto;
using SurfTicket.Application.Features.Venue.Query.GetAdminVenues.Dto;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Venue.Mapper
{
    public class VenueProfile : Profile
    {
        public VenueProfile()
        {
            CreateMap<VenueEntity, AdminVenueDetail>().ForMember(dest => dest.LogoUrl, opt => opt.MapFrom<VenueLogoResolver>());
            CreateMap<VenueEntity, AdminVenueItem>().ForMember(dest => dest.LogoUrl, opt => opt.MapFrom<VenueLogoResolver>());
        }
    }
}
