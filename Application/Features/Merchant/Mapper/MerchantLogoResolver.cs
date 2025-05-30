using AutoMapper;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Merchant.Mapper
{
    public class MerchantLogoResolver : IValueResolver<MerchantEntity, object, string?>
    {
        private readonly IConfiguration _configuration;

        public MerchantLogoResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? Resolve(MerchantEntity source, object destination, string? destMember, ResolutionContext context)
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
