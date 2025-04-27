using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Merchant> Merchant { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketBuyWindow> TicketBuyWindow { get; set; }
        public DbSet<TicketScanWindow> TicketScanWindow { get; set; }
        public DbSet<TicketEntry> TicketEntry { get; set; }
        public DbSet<TicketInvoice> TicketInvoice { get; set; }
        public DbSet<Venue> Venue { get; set; }
        public DbSet<VenueLocation> VenueLocation { get; set; }
    }
}
