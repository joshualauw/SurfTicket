using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Common;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IVenueRepository
    {
        public void Create(VenueEntity entity);
        public void Remove(VenueEntity entity);
        public Task<VenueEntity?> GetAsync(int venueId);
        public Task<PagedData<VenueEntity>> GetAdminVenues(int merchantId, FilterQuery filter);
    }
}
