using Newtonsoft.Json;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.JsonSchema;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.PermissionAdmin.Any())
            {
                List<PermissionAdminEntity> permissions = new List<PermissionAdminEntity>();

                foreach (PermissionCode code in Enum.GetValues(typeof(PermissionCode))) 
                {
                    var input = code.ToString().ToLower();

                    permissions.Add(new PermissionAdminEntity()
                    {
                        Name = char.ToUpper(input[0]) + input.Substring(1),
                        Code = code,
                    });
                }

                context.PermissionAdmin.AddRange(permissions);
            }

            if (!context.Plan.Any())
            {
                    context.Plan.AddRange(
                    new PlanEntity()
                    {
                        Name = "Basic Plan",
                        Code = PlanCode.BASIC,
                        Price = 0,
                        DayDuration = 0,
                        Features = new PlanFeature()
                        {
                            MaxOwnedMerchant = 1,
                            MaxCollabMerchant = 1,
                            ReportLevel = 0,
                            MaxTicketsPerMerchant = 3
                        }
                    },
                    new PlanEntity()
                    {
                        Name = "Starter Plan",
                        Code = PlanCode.STARTER,
                        Price = 149000,
                        DayDuration = 30,
                        Features = new PlanFeature()
                        {
                            MaxOwnedMerchant = 2,
                            MaxCollabMerchant = 3,
                            ReportLevel = 1,
                            MaxTicketsPerMerchant = 6
                        }
                    },
                    new PlanEntity()
                    {
                        Name = "Pro Plan",
                        Code = PlanCode.PRO,
                        Price = 249000,
                        DayDuration = 30,
                        Features = new PlanFeature()
                        {
                            MaxOwnedMerchant = 3,
                            MaxCollabMerchant = 5,
                            ReportLevel = 1,
                            MaxTicketsPerMerchant = 10
                        }
                    }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}
