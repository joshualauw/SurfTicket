using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IMerchantUserRepository
    {
        public Task CreateAsync(MerchantUserEntity entity, EntityAudit? audit = null);
        public Task<MerchantUserEntity?> GetMerchantUserAsync(int merchantId, string userId);
        public Task<bool> HasPermissionAsync(MerchantUserEntity entity, PermissionCode code, PermissionAccess access);
        public Task<List<PermissionMenuEntity>> AssignPermissionAsync(int merchantUserId, EntityAudit? audit = null);
    }
}
