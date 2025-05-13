using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IMerchantUserRepository
    {
        public Task<int> GetUserMerchantCountAsync(string userId);
        public Task<MerchantUserEntity?> CreateAsync(MerchantUserEntity entity);
        public Task<List<PermissionMenuEntity>> AssignOwnerPermissionAsync(int merchantUserId);
    }
}
