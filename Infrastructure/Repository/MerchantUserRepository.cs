using Microsoft.EntityFrameworkCore;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Data;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Infrastructure.Repository
{
    public class MerchantUserRepository : IMerchantUserRepository
    {
        private readonly AppDbContext _dbContext;

        public MerchantUserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(MerchantUserEntity entity)
        {
            _dbContext.MerchantUser.Add(entity);
        }

        public async Task<List<MerchantUserEntity>> GetMerchantByRoleAsync(string userId, MerchantRole role)
        {
            return await _dbContext.MerchantUser
            .Where(mu => mu.UserId == userId && mu.Role == role)
            .ToListAsync();
        }

        public async Task<MerchantUserEntity?> GetMerchantUserAsync(int merchantId, string userId)
        {
            return await _dbContext.MerchantUser
            .Where(mu => mu.MerchantId == merchantId && mu.UserId == userId)
            .Include(mu => mu.PermissionMenus)
            .FirstOrDefaultAsync();
        }

        public async Task<List<PermissionMenuEntity>> GetMerchantUserPermissionsAsync(int merchantUserId)
        {
            return await _dbContext.PermissionMenu
            .Where(pm => pm.MerchantUserId == merchantUserId)
            .Include(pm => pm.PermissionAdmin)
            .ToListAsync();
        }
    }
}
