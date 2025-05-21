using Microsoft.EntityFrameworkCore;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Data;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Infrastructure.Repository
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly AppDbContext _dbContext;
        public MerchantRepository(AppDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public void Create(MerchantEntity entity)
        {    
            _dbContext.Merchant.Add(entity);
        }

        public async Task<List<MerchantEntity>> GetMerchantsByRoleAsync(string userId, MerchantRole role)
        {
            var merchants = await _dbContext.MerchantUser
            .Where(mu => mu.UserId == userId && mu.Role == role)
            .Include(mu => mu.Merchant)
            .Select(mu => mu.Merchant)
            .ToListAsync();

            return merchants;
        }
    }
}
