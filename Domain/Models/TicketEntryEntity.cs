using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class TicketEntryEntity : BaseEntity
    {
        public int TicketPurchaseId { get; set; }
        public TicketPurchaseEntity TicketPurchase { get; set; }
        public string ScanCode { get; set; }
        public bool IsScanned { get; set; } = false;
        public DateTime? ScannedAt { get; set; }
    }
}
