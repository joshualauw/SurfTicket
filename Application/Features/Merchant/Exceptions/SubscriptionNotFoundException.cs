using SurfTicket.Application.Exceptions;

namespace SurfTicket.Application.Features.Merchant.Exceptions
{
    public class SubscriptionNotFoundException : SurfException
    {
        public SubscriptionNotFoundException() : base(SurfErrorCode.SUBSCRIPTION_NOT_FOUND, "Active subscription not found", 404) { }
    }
}
