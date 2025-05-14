using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IMerchantRepository
    {
        public Task<MerchantEntity?> CreateAsync(MerchantEntity entity);
        public Task<List<MerchantEntity>> GetMerchantsByRoleAsync(string userId, MerchantRole role);
    }
}
