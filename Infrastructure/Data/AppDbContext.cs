using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<UserEntity, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MerchantEntity> Merchant { get; set; }
        public DbSet<TicketEntity> Ticket { get; set; }
        public DbSet<TicketBuyWindowEntity> TicketBuyWindow { get; set; }
        public DbSet<TicketScanWindowEntity> TicketScanWindow { get; set; }
        public DbSet<TicketEntryEntity> TicketEntry { get; set; }
        public DbSet<TicketInvoiceEntity> TicketInvoice { get; set; }
        public DbSet<VenueEntity> Venue { get; set; }
        public DbSet<VenueLocationEntity> VenueLocation { get; set; }
        public DbSet<PermissionAdminEntity> PermissionAdmin { get; set; }
        public DbSet<PermissionMenuEntity> PermissionMenu { get; set; }
        public DbSet<MerchantUserEntity> MerchantUser { get; set; }
        public DbSet<PlanEntity> Plan { get; set; }
        public DbSet<SubscriptionEntity> Subscription { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MerchantEntity>().ToTable("Merchant");
            builder.Entity<TicketEntity>().ToTable("Ticket");
            builder.Entity<TicketBuyWindowEntity>().ToTable("TicketBuyWindow");
            builder.Entity<TicketScanWindowEntity>().ToTable("TicketScanWindow");
            builder.Entity<TicketEntryEntity>().ToTable("TicketEntry");
            builder.Entity<TicketInvoiceEntity>().ToTable("TicketInvoice");
            builder.Entity<VenueEntity>().ToTable("Venue");
            builder.Entity<PermissionAdminEntity>().ToTable("PermissionAdmin");
            builder.Entity<PermissionMenuEntity>().ToTable("PermissionMenu");
            builder.Entity<MerchantUserEntity>().ToTable("MerchantUser");
            builder.Entity<PlanEntity>().ToTable("Plan");
            builder.Entity<SubscriptionEntity>().ToTable("Subscription");
        }
    }
}
