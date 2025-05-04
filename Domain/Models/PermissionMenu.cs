using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class PermissionMenu : BaseEntity
    {
        public int PermissionAdminId { get; set; }
        public PermissionAdmin PermissionAdmin { get; set; }
        public int MerchantUserId { get; set; }
        public MerchantUser MerchantUser { get; set; }
        public PermissionType Access { get; set; }
    }
}
