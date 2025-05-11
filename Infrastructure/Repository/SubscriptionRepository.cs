using Microsoft.EntityFrameworkCore;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Data;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Infrastructure.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _dbContext;
        public SubscriptionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SubscriptionEntity?> CreateAsync(SubscriptionEntity entity)
        {
            _dbContext.Subscription.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
