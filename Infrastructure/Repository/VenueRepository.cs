using Microsoft.EntityFrameworkCore;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Common;
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

        public void Create(VenueEntity entity)
        {
            _dbContext.Venue.Add(entity);
        }

        public async Task<PagedResult<VenueEntity>> GetPagedAdminVenues(int merchantId, int page, int size)
        {
            return await _dbContext.Venue
            .Where(v => v.MerchantId == merchantId)
            .ToPagedResultAsync(page, size);
        }
    }
}
