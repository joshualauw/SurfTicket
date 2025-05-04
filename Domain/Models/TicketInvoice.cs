using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class TicketInvoice : BaseEntity
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public double Total { get; set; }
        public TicketInvoiceStatus Status { get; set; }
        public DateTime PurchasedAt { get; set; }
        public List<TicketEntry> TicketEntries { get; set; }
    }
}
