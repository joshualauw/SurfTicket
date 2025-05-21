using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class MerchantEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? LogoUrl { get; set; }
        public List<VenueEntity> Venues { get; set; }
        public virtual List<MerchantUserEntity> MerchantUsers { get; set; }

        public void AddOwner(string userId)
        {     
            MerchantUsers.Add(new MerchantUserEntity() {
                UserId = userId,
                Role = MerchantRole.OWNER,
            });
        }
    }
}
