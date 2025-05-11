namespace SurfTicket.Domain.Models
{
    public class TicketBuyWindowEntity : BaseEntity
    {
        public int TicketId { get; set; }
        public TicketEntity Ticket { get; set; }
        public int DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
