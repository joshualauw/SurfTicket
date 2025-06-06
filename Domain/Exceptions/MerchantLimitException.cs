namespace SurfTicket.Domain.Exceptions
{
    public class MerchantLimitException : Exception
    {
        public MerchantLimitException() : base("Maximum merchant limit created") { }
    }
}
