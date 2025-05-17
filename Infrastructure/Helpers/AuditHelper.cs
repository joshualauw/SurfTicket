using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Helpers
{
    public static class AuditHelper
    {
        public static void CreatedBy(BaseEntity entity, EntityAudit? audit) 
        {
            if (audit != null && audit.CreatedBy != null)
            {
                entity.CreatedBy = audit.CreatedBy;
            }
        }
    }
}
