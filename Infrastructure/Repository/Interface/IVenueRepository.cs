using SurfTicket.Application.Features.Venue.Query.GetAdminVenues.Dto;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IVenueRepository
    {
        public void Create(VenueEntity entity);
        public IQueryable<VenueEntity> GetAdminVenues(int merchantId);
    }
}
