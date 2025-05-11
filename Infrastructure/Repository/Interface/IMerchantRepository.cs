using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IMerchantRepository
    {
        public Task<MerchantEntity?> CreateAsync(MerchantEntity entity);
    }
}
