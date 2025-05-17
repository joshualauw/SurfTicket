using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Data;
using SurfTicket.Infrastructure.Helpers;
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

        public void Create(VenueEntity entity, EntityAudit? audit = null)
        {
            AuditHelper.CreatedBy(entity, audit);

            _dbContext.Venue.Add(entity);
        }
    }
}
