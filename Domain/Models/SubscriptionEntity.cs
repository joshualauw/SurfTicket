using SurfTicket.Domain.Exceptions;

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

        public static SubscriptionEntity Create(int planId, string userId)
        {
            return new SubscriptionEntity()
            {
                PlanId = planId,
                UserId = userId,
                StartAt = DateTime.UtcNow,
                IsActive = true
            };
        }

        public void EnsureCanCreateMerchant(int ownedMerchantCount)
        {
            if (Plan.Features.MaxOwnedMerchant <= ownedMerchantCount)
            {
                throw new MerchantLimitException();
            }
        }
    }
}
