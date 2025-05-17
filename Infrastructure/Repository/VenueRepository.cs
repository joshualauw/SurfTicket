using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Data;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Infrastructure.Repository
{
    public class VenueRepository : IVenueRepository
    {
        private readonly AppDbContext _dbContext;

        public VenueRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(VenueEntity entity, EntityAudit? audit = null)
        {
            if (audit != null && audit.CreatedBy != null)
            {
                entity.CreatedBy = audit.CreatedBy;
            }

            _dbContext.Venue.Add(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
