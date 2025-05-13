using SurfTicket.Domain.Enums;
using SurfTicket.Domain.JsonSchema;

namespace SurfTicket.Domain.Models
{
    public class PlanEntity : BaseEntity
    {
        public string Name { get; set; }
        public PlanCode Code { get; set; }
        public double Price { get; set; }
        public int DayDuration { get; set; }
        public PlanFeature Features { get; set; }
        public List<SubscriptionEntity> Subscriptions { get; set; }
    }
}
