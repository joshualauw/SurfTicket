namespace SurfTicket.Domain.Models
{
    public class MerchantEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? LogoUrl { get; set; }
        public List<VenueEntity> Venues { get; set; }
        public List<MerchantUserEntity> MerchantUsers { get; set; }
    }
}
