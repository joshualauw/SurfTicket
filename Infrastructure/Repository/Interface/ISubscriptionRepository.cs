using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface ISubscriptionRepository
    {
        public void Create(SubscriptionEntity entity, EntityAudit? audit = null);
        public Task<SubscriptionEntity?> GetUserActiveSubscriptionAsync(string userId);
    }
}
