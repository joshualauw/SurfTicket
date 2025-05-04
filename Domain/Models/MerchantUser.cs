using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class MerchantUser : BaseEntity
    {
        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public MerchantRole Role { get; set; }
        public List<PermissionMenu> PermissionMenus { get; set; }
    }
}
