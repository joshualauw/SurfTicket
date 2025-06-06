using SurfTicket.Application.Exceptions;

namespace SurfTicket.Application.Features.Merchant.Exceptions
{
    public class MerchantUserNotFoundException : SurfException
    {
        public MerchantUserNotFoundException() : base(SurfErrorCode.MERCHANT_NOT_FOUND, "Merchant user not found", 404)
        {
        }
    }
}
