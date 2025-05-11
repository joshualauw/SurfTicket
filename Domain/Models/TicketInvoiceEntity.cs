using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class TicketInvoiceEntity : BaseEntity
    {
        public int TicketId { get; set; }
        public TicketEntity Ticket { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        public double Total { get; set; }
        public TicketInvoiceStatus Status { get; set; }
        public DateTime PurchasedAt { get; set; }
        public List<TicketEntryEntity> TicketEntries { get; set; }
    }
}
