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

        public async Task<SubscriptionEntity?> GetUserActiveSubscriptionAsync(string userId)
        {
            return await _dbContext.Subscription.Where(s => s.UserId == userId && s.IsActive).Include(s => s.Plan).FirstOrDefaultAsync();
        }
    }
}
