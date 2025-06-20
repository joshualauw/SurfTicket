using System.Linq.Dynamic.Core;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Common;
using SurfTicket.Infrastructure.Data;
using SurfTicket.Infrastructure.Extensions;
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

        public void Remove(VenueEntity entity)
        {
           _dbContext.Venue.Remove(entity);
        }

        public async Task<PagedData<VenueEntity>> GetAdminVenues(int merchantId, FilterQuery filter)
        {
            List<string> whitelistFields = new List<string>() { "Name" };

            var query = await _dbContext.Venue
            .Where(v => v.MerchantId == merchantId)
            .ApplyFilters(filter.FilterBy, whitelistFields)
            .ApplySorting(filter.SortBy, filter.SortOrder, whitelistFields)
            .ToPagedResultAsync(filter.Page, filter.Size);

            return query;
        }

        public async Task<VenueEntity?> GetAsync(int venueId)
        {
            return await _dbContext.Venue.FindAsync(venueId);
        }
    }
}
