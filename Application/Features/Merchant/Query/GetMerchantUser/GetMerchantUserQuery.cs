using MediatR;

namespace SurfTicket.Application.Features.Merchant.Query.GetMerchantUser
{
    public class GetMerchantUserQuery : IRequest<GetMerchantUserQueryResponse>
    {
        public int MerchantId { get; set; }
    }
}
