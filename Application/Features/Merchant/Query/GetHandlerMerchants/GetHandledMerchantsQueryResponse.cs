using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Merchant.Query.GetHandlerMerchants
{
    public class GetHandledMerchantsQueryResponse
    {
        public List<MerchantEntity> OwnedMerchants { get; set; }
        public List<MerchantEntity> CollaboratedMerchants { get; set; }
    }
}
