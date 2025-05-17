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
                            MaxCollabMerchant = 3,
                            ReportLevel = 0,
                            MaxTicketsPerMerchant = 5
                        }
                    },
                    new PlanEntity()
                    {
                        Name = "Starter Plan",
                        Code = PlanCode.STARTER,
                        Price = 59000,
                        DayDuration = 30,
                        Features = new PlanFeature()
                        {
                            MaxOwnedMerchant = 3,
                            MaxCollabMerchant = 10,
                            ReportLevel = 1,
                            MaxTicketsPerMerchant = 0
                        }
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
