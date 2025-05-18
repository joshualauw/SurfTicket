using SurfTicket.Domain.Enums;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IPermissionAdminRepository
    {
        public Task<PermissionAdminEntity?> GetByCodeAsync(PermissionCode code);
    }
}
