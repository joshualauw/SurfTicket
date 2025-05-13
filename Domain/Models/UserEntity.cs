using Microsoft.AspNetCore.Identity;

namespace SurfTicket.Domain.Models
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? VerifyCode { get; set; }
        public List<TicketPurchaseEntity> TicketPurchases { get; set; }
        public List<MerchantUserEntity> MerchantUsers { get; set; }
    }
}
