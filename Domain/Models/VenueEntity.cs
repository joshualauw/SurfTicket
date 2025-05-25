namespace SurfTicket.Domain.Models
{
    public class VenueEntity : BaseEntity
    {
        public int? VenueLocationId { get; set; }
        public VenueLocationEntity? VenueLocation { get; set; }
        public int MerchantId { get; set; }
        public MerchantEntity Merchant { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? LogoUrl { get; set; }
        public List<TicketEntity> Tickets { get; set; }

        public static VenueEntity Create(int merchantId, string name, string description)
        {
            return new VenueEntity()
            {
                MerchantId = merchantId,
                Name = name,
                Description = description
            };
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
