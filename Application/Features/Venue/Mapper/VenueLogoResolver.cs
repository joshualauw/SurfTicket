using AutoMapper;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Venue.Mapper
{
    public class VenueLogoResolver : IValueResolver<VenueEntity, object, string?>
    {
        private readonly IConfiguration _configuration;

        public VenueLogoResolver(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }

        public string? Resolve(VenueEntity source, object destination, string? destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.LogoUrl))
            {
                return null;
            }

            var baseUrl = _configuration.GetValue<string>("App:BaseUrl");

            return $"{baseUrl}/{source.LogoUrl}";
        }
    }
}
