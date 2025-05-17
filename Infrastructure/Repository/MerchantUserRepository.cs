using Microsoft.EntityFrameworkCore;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Data;
using SurfTicket.Infrastructure.Helpers;
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

        public void Create(MerchantUserEntity entity, EntityAudit? audit = null)
        {
            AuditHelper.CreatedBy(entity, audit);

            _dbContext.MerchantUser.Add(entity);
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
                var permission = await _dbContext.PermissionAdmin.FirstOrDefaultAsync(p => p.Code == code);
                if (permission == null)
                {
                    return false;
                }

                return await _dbContext.PermissionMenu
                .Where(pm => pm.MerchantUserId == entity.Id && pm.PermissionAdminId == permission.Id && pm.Access == access)
                .AnyAsync();
            }
            else
            {
                return false;
            }
        }
    }
}
