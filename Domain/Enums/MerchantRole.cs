using System.Runtime.Serialization;

namespace SurfTicket.Domain.Enums
{
    public enum MerchantRole
    {
        [EnumMember(Value = "owner")]
        OWNER,

        [EnumMember(Value = "collaborator")]
        COLLABORATOR,
    }
}
