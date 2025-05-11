using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class TicketEntryEntity : BaseEntity
    {
        public int TicketInvoiceId { get; set; }
        public TicketInvoiceEntity TicketInvoice { get; set; }
        public string ScanCode { get; set; }
        public TicketScanStatus Status { get; set; }
        public DateTime? ScannedAt { get; set; }
    }
}
