using System.Runtime.Serialization;

namespace SurfTicket.Domain.Enums
{
    public enum TicketInvoiceStatus
    {
        [EnumMember(Value = "pending")]
        PENDING,

        [EnumMember(Value = "success")]
        SUCCESS,

        [EnumMember(Value = "failed")]
        FAILED,

        [EnumMember(Value = "refunded")]
        REFUNDED
    }
}
