using Microsoft.EntityFrameworkCore;
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

        public async Task<MerchantEntity?> CreateAsync(MerchantEntity entity)
        {
            _dbContext.Merchant.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
