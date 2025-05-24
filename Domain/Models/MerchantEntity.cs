using MediatR;
using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class MerchantEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? LogoUrl { get; set; }
        public List<VenueEntity> Venues { get; set; }
        public List<MerchantUserEntity> MerchantUsers { get; set; }

        public static MerchantEntity Create(string name, string description, string userId)
        {
            return new MerchantEntity()
            {
                Name = name,
                Description = description,
                MerchantUsers = new List<MerchantUserEntity>()
                {
                    new MerchantUserEntity()
                    {
                        UserId = userId,
                        Role = MerchantRole.OWNER
                    }
                }
            };
        }
    }
}
