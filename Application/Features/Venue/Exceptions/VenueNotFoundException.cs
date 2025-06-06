using SurfTicket.Application.Exceptions;

namespace SurfTicket.Application.Features.Venue.Exceptions
{
    public class VenueNotFoundException : SurfException
    {
        public VenueNotFoundException() : base(SurfErrorCode.VENUE_NOT_FOUND, "Venue not found", 404)
        {
        }
    }
}
