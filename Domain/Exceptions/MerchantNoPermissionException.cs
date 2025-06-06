namespace SurfTicket.Domain.Exceptions
{
    public class MerchantNoPermissionException : Exception
    {
        public MerchantNoPermissionException() : base("Merchant doesn't have permission")
        {
        }
    }
}
