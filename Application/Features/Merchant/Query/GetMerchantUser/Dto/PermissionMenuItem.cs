using SurfTicket.Domain.Enums;

namespace SurfTicket.Application.Features.Merchant.Query.GetMerchantUser.Dto
{
    public class PermissionMenuItem
    {
        public int Id { get; set; }
        public PermissionCode Code { get; set; }
        public PermissionAccess Access { get; set; }
    }
}
