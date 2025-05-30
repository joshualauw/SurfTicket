namespace SurfTicket.Application.Features.Venue.Query.GetAdminVenues.Dto
{
    public class AdminVenueItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LogoUrl { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
