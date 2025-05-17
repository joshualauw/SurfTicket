using System.Runtime.Serialization;

namespace SurfTicket.Domain.Enums
{
    public enum PermissionCode
    {
        [EnumMember(Value = "dashboard")]
        DASHBOARD,

        [EnumMember(Value = "venue")]
        VENUE,

        [EnumMember(Value = "ticket")]
        TICKET,

        [EnumMember(Value = "scan")]
        SCAN,

        [EnumMember(Value = "chat")]
        CHAT,

        [EnumMember(Value = "settings")]
        SETTINGS
    }
}
