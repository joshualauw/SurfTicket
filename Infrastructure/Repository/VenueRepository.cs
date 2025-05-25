using System.Linq.Dynamic.Core;
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

        public void Remove(VenueEntity entity)
        {
           _dbContext.Venue.Remove(entity);
        }

        public async Task<Common.PagedResult<VenueEntity>> GetAdminVenues(int merchantId, FilterQuery filter)
        {
            var query = _dbContext.Venue.Where(v => v.MerchantId == merchantId);

            if (filter.FilterBy != null)
            {
                foreach (var f in filter.FilterBy)
                {
                    if (!string.IsNullOrEmpty(f.Value) && f.Key != null)
                    {
                        query = query.Where($"{f.Key}.ToLower().Contains(@0)", f.Value.ToLower());
                    }
                }
            }
      
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                query = query.OrderBy($"{filter.SortBy} {filter.SortOrder}");
            }

            return await query.ToPagedResultAsync(filter.Page, filter.Size);
        }

        public async Task<VenueEntity?> GetAsync(int venueId)
        {
            return await _dbContext.Venue.FindAsync(venueId);
        }
    }
}
