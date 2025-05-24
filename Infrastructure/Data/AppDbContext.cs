using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;
using System.Reflection.Emit;

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

            //Ovveride Identity table names
            builder.Entity<UserEntity>().ToTable("users_entity");
            builder.Entity<IdentityRole>().ToTable("roles_entity");
            builder.Entity<IdentityUserRole<string>>().ToTable("user_roles_entity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("user_claims_entity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("user_logins_entity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims_entity");
            builder.Entity<IdentityUserToken<string>>().ToTable("user_tokens_entity");

            // Composite Keys
            builder.Entity<MerchantUserEntity>().HasIndex(mu => new {mu.MerchantId, mu.UserId}).IsUnique();

            // Field Json
            builder.Entity<PlanEntity>().OwnsOne(p => p.Features, f => { f.ToJson(); });

            // Field Enum
            builder.Entity<MerchantUserEntity>().Property(p => p.Role).HasConversion<string>();
            builder.Entity<PermissionMenuEntity>().Property(p => p.Access).HasConversion<string>();
            builder.Entity<PermissionAdminEntity>().Property(p => p.Code).HasConversion<string>();
            builder.Entity<TicketPurchaseEntity>().Property(p => p.Status).HasConversion<string>();
            builder.Entity<PlanEntity>().Property(p => p.Code).HasConversion<string>();
        }
    }
}
