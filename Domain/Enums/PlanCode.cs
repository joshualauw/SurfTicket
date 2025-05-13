using System.Runtime.Serialization;

namespace SurfTicket.Domain.Enums
{
    public enum PlanCode
    {
        [EnumMember(Value = "basic")]
        BASIC,

        [EnumMember(Value = "starter")]
        STARTER
    }
}
