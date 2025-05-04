namespace SurfTicket.Domain.Models
{
    public class TicketBuyWindow : BaseEntity
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
