using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IVenueRepository
    {
        public Task CreateAsync(VenueEntity entity, EntityAudit? audit = null);
    }
}
