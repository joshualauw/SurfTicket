using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class PermissionMenuEntity : BaseEntity
    {
        public int MerchantUserId { get; set; }
        public MerchantUserEntity MerchantUser { get; set; }
        public int PermissionAdminId { get; set; }
        public PermissionAdminEntity PermissionAdmin { get; set; }
        public PermissionAccess Access { get; set; }
    }
}
