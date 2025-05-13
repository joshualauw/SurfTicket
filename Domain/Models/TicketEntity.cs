namespace SurfTicket.Domain.Models
{
    public class TicketEntity : BaseEntity
    {
        public int VenueId { get; set; }
        public VenueEntity Venue { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = false;
        public int Quantity { get; set; }
        public bool EnableBuyAnytime { get; set; } = false;
        public int OneTimeBuyLimit { get; set; }
        public DateTime? CanBuyFrom { get; set; }
        public DateTime? CanBuyUntil { get; set; }
        public bool EnableScanAnytime { get; set; } = false;
        public DateTime CanScanFrom { get; set; }
        public DateTime CanScanUntil { get; set; }
        public List<TicketBuyWindowEntity> TicketBuyWindows { get; set; }
        public List<TicketScanWindowEntity> TicketScanWindows { get; set; }
        public List<TicketPurchaseEntity> TicketPurchases { get; set; }
    }
}
