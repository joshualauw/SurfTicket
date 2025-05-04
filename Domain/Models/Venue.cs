namespace SurfTicket.Domain.Models
{
    public class Venue : BaseEntity
    {
        public int VenueLocationId { get; set; }
        public VenueLocation VenueLocation { get; set; }
        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? LogoUrl { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
