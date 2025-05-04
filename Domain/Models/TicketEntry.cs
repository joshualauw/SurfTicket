using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class TicketEntry : BaseEntity
    {
        public int TicketInvoiceId { get; set; }
        public TicketInvoice TicketInvoice { get; set; }
        public string ScanCode { get; set; }
        public TicketScanStatus Status { get; set; }
        public DateTime? ScannedAt { get; set; }
    }
}
