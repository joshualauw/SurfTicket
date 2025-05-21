using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IMerchantUserRepository
    {
        public Task<MerchantUserEntity?> GetMerchantUserAsync(int merchantId, string userId);
        public Task<List<MerchantUserEntity>> GetMerchantByRoleAsync(string userId, MerchantRole role);
        public Task<List<PermissionMenuEntity>> GetMerchantUserPermissionsAsync(int merchantUserId);
    }
}
