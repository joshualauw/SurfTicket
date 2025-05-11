namespace SurfTicket.Domain.Models
{
    public class VenueLocationEntity : BaseEntity
    {
        public string CountryCode { get; set; }
        public string Address { get; set; }
        public string StreetName { get; set; }
        public string ZipCode { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
    }
}
