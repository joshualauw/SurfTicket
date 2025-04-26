using SurfTicket.Enums;

namespace SurfTicket.Models
{
    public class TicketInvoice : BaseEntity
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public double Total {  get; set; }
        public TicketInvoiceStatus Status { get; set; }
        public DateTime PurchasedAt { get; set; }
        public List<TicketEntry> TicketEntries { get; set; } = null!;
    }
}
