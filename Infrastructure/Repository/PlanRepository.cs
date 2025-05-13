using Microsoft.EntityFrameworkCore;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Data;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Infrastructure.Repository
{
    public class PlanRepository : IPlanRepository
    {
        private readonly AppDbContext _dbContext;
        public PlanRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PlanEntity?> GetPlanByCodeAsync(PlanCode code)
        {
            return await _dbContext.Plan.FirstOrDefaultAsync(p => p.Code == code);
        }
    }
}
