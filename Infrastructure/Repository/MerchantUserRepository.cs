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

        public async Task<List<PermissionMenuEntity>> AssignOwnerPermissionAsync(int merchantUserId)
        {
            var existingPermissions = await _dbContext.PermissionMenu.Where(pm => pm.MerchantUserId == merchantUserId).ToListAsync();
            if (existingPermissions.Any())
            {
                _dbContext.PermissionMenu.RemoveRange(existingPermissions);
                await _dbContext.SaveChangesAsync();
            }

            var permissions = await _dbContext.PermissionAdmin.ToListAsync();

            List<PermissionMenuEntity> permissionMenus = new List<PermissionMenuEntity>();

            foreach (var p in permissions)
            {
                permissionMenus.Add(new PermissionMenuEntity() { PermissionAdminId = p.Id, MerchantUserId = merchantUserId, Access = PermissionAccess.VIEW });
                permissionMenus.Add(new PermissionMenuEntity() { PermissionAdminId = p.Id, MerchantUserId = merchantUserId, Access = PermissionAccess.INSERT });
                permissionMenus.Add(new PermissionMenuEntity() { PermissionAdminId = p.Id, MerchantUserId = merchantUserId, Access = PermissionAccess.UPDATE });
                permissionMenus.Add(new PermissionMenuEntity() { PermissionAdminId = p.Id, MerchantUserId = merchantUserId, Access = PermissionAccess.DELETE });
            }

            _dbContext.PermissionMenu.AddRange(permissionMenus);
            await _dbContext.SaveChangesAsync();

            return permissionMenus;
        }

        public async Task<MerchantUserEntity?> CreateAsync(MerchantUserEntity entity)
        {
            _dbContext.MerchantUser.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<int> GetUserMerchantCountAsync(string userId)
        {
            return await _dbContext.MerchantUser.Where(mu => mu.UserId == userId && mu.Role == MerchantRole.OWNER).CountAsync();
        }
    }
}
