namespace SurfTicket.Domain.Models
{
    public class Merchant : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public List<Venue> Venues { get; set; }
    }
}
