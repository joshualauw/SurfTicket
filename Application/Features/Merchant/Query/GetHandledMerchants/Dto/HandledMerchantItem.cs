namespace SurfTicket.Application.Features.Merchant.Query.GetHandledMerchants.Dto
{
    public class HandledMerchantItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LogoUrl { get; set; }
        public DateTime LastVisited { get; set; }
    }
}
