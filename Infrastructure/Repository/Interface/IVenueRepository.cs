using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Common;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IVenueRepository
    {
        public void Create(VenueEntity entity);
        public Task<PagedResult<VenueEntity>> GetAdminVenues(int merchantId, FilterQuery filter);
    }
}
