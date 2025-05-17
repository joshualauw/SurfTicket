namespace SurfTicket.Domain.Models
{
    public class VenueEntity : BaseEntity
    {
        public int? VenueLocationId { get; set; }
        public VenueLocationEntity? VenueLocation { get; set; }
        public int MerchantId { get; set; }
        public MerchantEntity Merchant { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? LogoUrl { get; set; }
        public List<TicketEntity> Tickets { get; set; }
    }
}
