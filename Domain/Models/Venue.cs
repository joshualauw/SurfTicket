namespace SurfTicket.Domain.Models
{
    public class Venue : BaseEntity
    {
        public int VenueLocationId { get; set; }
        public VenueLocation VenueLocation { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
