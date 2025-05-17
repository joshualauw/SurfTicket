using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurfTicket.Domain.Enums;
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
        public DbSet<TicketPurchaseEntity> TicketPurchase { get; set; }
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

            // Table Naming
            builder.Entity<MerchantEntity>().ToTable("Merchant");
            builder.Entity<TicketEntity>().ToTable("Ticket");
            builder.Entity<TicketBuyWindowEntity>().ToTable("TicketBuyWindow");
            builder.Entity<TicketScanWindowEntity>().ToTable("TicketScanWindow");
            builder.Entity<TicketEntryEntity>().ToTable("TicketEntry");
            builder.Entity<TicketPurchaseEntity>().ToTable("TicketPurchase");
            builder.Entity<VenueEntity>().ToTable("Venue");
            builder.Entity<PermissionAdminEntity>().ToTable("PermissionAdmin");
            builder.Entity<PermissionMenuEntity>().ToTable("PermissionMenu");
            builder.Entity<MerchantUserEntity>().ToTable("MerchantUser");
            builder.Entity<PlanEntity>().ToTable("Plan");
            builder.Entity<SubscriptionEntity>().ToTable("Subscription");

            // Composite Keys
            builder.Entity<MerchantUserEntity>().HasIndex(mu => new {mu.MerchantId, mu.UserId}).IsUnique();

            // Field Json
            builder.Entity<PlanEntity>().OwnsOne(p => p.Features, f => { f.ToJson(); });

            // Field Enum
            builder.Entity<MerchantUserEntity>().Property(p => p.Role).HasConversion(
                v => v.ToString(),
                v => (MerchantRole) Enum.Parse(typeof (MerchantRole), v)
            );
            builder.Entity<PermissionMenuEntity>().Property(p => p.Access).HasConversion(
                v => v.ToString(),
                v => (PermissionAccess) Enum.Parse(typeof(PermissionAccess), v)
            );
            builder.Entity<PermissionAdminEntity>().Property(p => p.Code).HasConversion(
                v => v.ToString(),
                v => (PermissionCode) Enum.Parse(typeof (PermissionCode), v)
            );
            builder.Entity<TicketPurchaseEntity>().Property(p => p.Status).HasConversion(
                v => v.ToString(),
                v => (TicketInvoiceStatus) Enum.Parse(typeof(TicketInvoiceStatus), v)
            );
            builder.Entity<PlanEntity>().Property(p => p.Code).HasConversion(
                v => v.ToString(),
                v => (PlanCode) Enum.Parse(typeof(PlanCode), v)
            );
        }
    }
}
