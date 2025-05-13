namespace SurfTicket.Domain.Models
{
    public class PermissionAdminEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Note { get; set; }
        public List<PermissionMenuEntity> permissionMenus { get; set; }
    }
}
