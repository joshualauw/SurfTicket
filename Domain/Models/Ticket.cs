namespace SurfTicket.Domain.Models
{
    public class Ticket : BaseEntity
    {
        public int VenueId { get; set; }
        public Venue Venue { get; set; }
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
        public List<TicketBuyWindow> TicketBuyWindows { get; set; }
        public List<TicketScanWindow> TicketScanWindows { get; set; }
        public List<TicketInvoice> TicketInvoices { get; set; }
    }
}
