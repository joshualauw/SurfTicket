using SurfTicket.Application.Features.Merchant.Query.GetMerchantUser.Dto;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;

namespace SurfTicket.Application.Features.Merchant.Query.GetMerchantUser
{
    public class GetMerchantUserQueryResponse
    {
        public int Id { get; set; }
        public MerchantRole Role { get; set; }
        public List<PermissionMenuItem> Permissions { get; set; }
    }
}
