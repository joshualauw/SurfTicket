using Microsoft.AspNetCore.Identity;

namespace SurfTicket.Domain.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<TicketInvoice> TicketInvoices { get; set; }
        public List<MerchantUser> MerchantUsers { get; set; }
    }
}
