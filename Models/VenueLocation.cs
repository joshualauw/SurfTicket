namespace SurfTicket.Models
{
    public class VenueLocation : BaseEntity
    {
        public string CountryCode { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string StreetName { get; set; } = string.Empty;
        public string ZipCode {  get; set; } = string.Empty;
        public string? Longitude { get; set; }
        public string? Latitude {  get; set; }
    }
}
