using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface ISubscriptionRepository
    {
        public Task<SubscriptionEntity?> CreateAsync(SubscriptionEntity entity);
        public Task<SubscriptionEntity?> GetUserActiveSubscriptionAsync(string userId);
    }
}
