using SurfTicket.Application.Exceptions;

namespace SurfTicket.Application.Features.Merchant.Exceptions
{
    public class PlanNotFoundException : SurfException
    {
        public PlanNotFoundException() : base(SurfErrorCode.PLAN_NOT_FOUND, "Plan not found", 404)
        {
        }
    }
}
