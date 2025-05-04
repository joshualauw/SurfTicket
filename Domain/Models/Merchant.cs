namespace SurfTicket.Domain.Models
{
    public class Merchant : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? LogoUrl { get; set; }
        public List<Venue> Venues { get; set; }
        public List<MerchantUser> MerchantUsers { get; set; }
    }
}
