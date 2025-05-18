using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IMerchantUserRepository
    {
        public void Create(MerchantUserEntity entity, EntityAudit? audit = null);
        public Task<MerchantUserEntity?> GetMerchantUserAsync(int merchantId, string userId);
        public Task<List<MerchantUserEntity>> GetMerchantByRoleAsync(string userId, MerchantRole role);
        public Task<List<PermissionMenuEntity>> GetMerchantUserPermissionsAsync(int merchantUserId);
    }
}
