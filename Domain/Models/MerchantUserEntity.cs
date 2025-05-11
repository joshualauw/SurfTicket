using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class MerchantUserEntity : BaseEntity
    {
        public int MerchantId { get; set; }
        public MerchantEntity Merchant { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        public MerchantRole Role { get; set; }
        public List<PermissionMenuEntity> PermissionMenus { get; set; }
    }
}
