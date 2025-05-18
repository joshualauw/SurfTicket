using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IVenueRepository
    {
        public void Create(VenueEntity entity, EntityAudit? audit = null);
        public Task<List<VenueEntity>> GetAdminVenues(int merchantId);
    }
}
