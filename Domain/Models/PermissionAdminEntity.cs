using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class PermissionAdminEntity : BaseEntity
    {
        public string Name { get; set; }
        public PermissionCode Code { get; set; }
        public List<PermissionMenuEntity> PermissionMenus { get; set; }
    }
}
