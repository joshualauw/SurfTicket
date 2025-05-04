namespace SurfTicket.Domain.Models
{
    public class PermissionAdmin : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
        public List<PermissionMenu> permissionMenus { get; set; }
    }
}
