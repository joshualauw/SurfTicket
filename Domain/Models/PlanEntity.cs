namespace SurfTicket.Domain.Models
{
    public class PlanEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public int DayDuration { get; set; }
        public string Features { get; set; }
        public List<SubscriptionEntity> Subscriptions { get; set; }
    }
}
