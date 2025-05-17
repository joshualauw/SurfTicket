using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IMerchantRepository
    {
        public Task<MerchantEntity?> CreateAsync(MerchantEntity entity, EntityAudit? audit = null);
        public Task<List<MerchantEntity>> GetMerchantsByRoleAsync(string userId, MerchantRole role);
        public Task<int> GetMerchantsByRoleCountAsync(string userId, MerchantRole role);
    }
}
