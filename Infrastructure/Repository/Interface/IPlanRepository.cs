using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IPlanRepository
    {
        public Task<PlanEntity?> GetPlanByCodeAsync(PlanCode code);
    }
}
