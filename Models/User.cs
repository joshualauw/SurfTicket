using Microsoft.AspNetCore.Identity;

namespace SurfTicket.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<TicketInvoice> TicketInvoices { get; set; } = null!;
    }
}
