using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IMerchantRepository
    {
        public void Create(MerchantEntity entity);
        public Task<MerchantEntity?> Get(int merchantId);
        public Task<List<MerchantEntity>> GetMerchantsByRoleAsync(string userId, MerchantRole role);
    }
}
