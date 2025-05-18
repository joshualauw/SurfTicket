using Microsoft.EntityFrameworkCore;
using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;
using SurfTicket.Infrastructure.Data;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Infrastructure.Repository
{
    public class PermissionAdminRepository : IPermissionAdminRepository
    {
        private readonly AppDbContext _dbContext;

        public PermissionAdminRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PermissionAdminEntity?> GetByCodeAsync(PermissionCode code)
        {
            return await _dbContext.PermissionAdmin.FirstOrDefaultAsync(pa => pa.Code == code);
        }
    }
}
