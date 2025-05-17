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

        public async Task<List<PermissionMenuEntity>> AssignPermissionAsync(int merchantUserId, EntityAudit? audit = null)
        {
            var existingPermissions = await _dbContext.PermissionMenu.Where(pm => pm.MerchantUserId == merchantUserId).ToListAsync();
            if (existingPermissions.Any())
            {
                _dbContext.PermissionMenu.RemoveRange(existingPermissions);
                await _dbContext.SaveChangesAsync();
            }

            List<PermissionMenuEntity> permissionMenus = new List<PermissionMenuEntity>();

            foreach (PermissionCode code in Enum.GetValues(typeof(PermissionCode)))
            {
                foreach (PermissionAccess access in Enum.GetValues(typeof(PermissionAccess)))
                {
                    PermissionMenuEntity menu = new PermissionMenuEntity()
                    {
                        Code = code,
                        MerchantUserId = merchantUserId,
                        Access = access,
                    };
                    if (audit != null && audit.CreatedBy != null)
                    {
                        menu.CreatedBy = audit.CreatedBy;
                    }

                    permissionMenus.Add(menu);
                }
            }

            _dbContext.PermissionMenu.AddRange(permissionMenus);
            await _dbContext.SaveChangesAsync();

            return permissionMenus;
        }

        public async Task CreateAsync(MerchantUserEntity entity, EntityAudit? audit = null)
        {
            if (audit != null && audit.CreatedBy != null)
            {
                entity.CreatedBy = audit.CreatedBy;
            }

            _dbContext.MerchantUser.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<MerchantUserEntity?> GetMerchantUserAsync(int merchantId, string userId)
        {
            return await _dbContext.MerchantUser.FirstOrDefaultAsync(mu => mu.MerchantId == merchantId && mu.UserId == userId);
        }

        public async Task<bool> HasPermissionAsync(MerchantUserEntity entity, PermissionCode code, PermissionAccess access)
        {
            if (entity.Role == MerchantRole.OWNER)
            {
                return true;
            }
            else if (entity.Role == MerchantRole.COLLABORATOR)
            {
                return await _dbContext.PermissionMenu
                .Where(pm => pm.MerchantUserId == entity.Id && pm.Code == code && pm.Access == access)
                .AnyAsync();
            }
            else
            {
                return false;
            }
        }
    }
}
