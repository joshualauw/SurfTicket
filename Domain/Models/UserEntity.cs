using Microsoft.AspNetCore.Identity;

namespace SurfTicket.Domain.Models
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? VerifyCode { get; set; }
        public List<TicketInvoiceEntity> TicketInvoices { get; set; }
        public List<MerchantUserEntity> MerchantUsers { get; set; }
    }
}
