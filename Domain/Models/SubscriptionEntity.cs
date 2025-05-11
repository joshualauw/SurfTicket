namespace SurfTicket.Domain.Models
{
    public class SubscriptionEntity : BaseEntity
    {
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        public int PlanId { get; set; }
        public PlanEntity Plan { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public DateTime? CanceledAt { get; set; }
        public bool IsActive { get; set; }
    }
}
