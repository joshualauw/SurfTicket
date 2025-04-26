using SurfTicket.Enums;

namespace SurfTicket.Models
{
    public class TicketEntry : BaseEntity
    {
        public int TicketInvoiceId { get; set; }
        public TicketInvoice TicketInvoice { get; set; } = null!;
        public string ScanCode { get; set; } = string.Empty;
        public TicketScanStatus Status { get; set; }
        public DateTime? ScannedAt { get; set; }
    }
}
