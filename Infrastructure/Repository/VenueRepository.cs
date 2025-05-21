using Microsoft.EntityFrameworkCore;
using SurfTicket.Application.Features.Venue.Query.GetAdminVenues.Dto;
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

        public void Create(VenueEntity entity)
        {
            _dbContext.Venue.Add(entity);
        }

        public IQueryable<VenueEntity> GetAdminVenues(int merchantId)
        {
            return _dbContext.Venue.Where(v => v.MerchantId == merchantId).AsNoTracking();
        }
    }
}
