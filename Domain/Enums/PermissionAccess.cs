using System.Runtime.Serialization;

namespace SurfTicket.Domain.Enums
{
    public enum PermissionAccess
    {
        [EnumMember(Value = "view")]
        VIEW,

        [EnumMember(Value = "insert")]
        INSERT,

        [EnumMember(Value = "update")]
        UPDATE,

        [EnumMember(Value = "delete")]
        DELETE,
    }
}
