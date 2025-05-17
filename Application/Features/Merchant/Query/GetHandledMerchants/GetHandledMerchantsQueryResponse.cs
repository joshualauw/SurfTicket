using SurfTicket.Application.Features.Merchant.Query.GetHandledMerchants.Dto;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Merchant.Query.GetHandlerMerchants
{
    public class GetHandledMerchantsQueryResponse
    {
        public List<HandledMerchantItem> OwnedMerchants { get; set; }
        public List<HandledMerchantItem> CollaboratedMerchants { get; set; }
    }
}
