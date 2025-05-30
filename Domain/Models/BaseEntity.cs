namespace SurfTicket.Domain.Models
{
    public class BaseEntity : EntityAudit
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false;
    }

    public class EntityAudit
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = "System";
        public string UpdatedBy { get; set; } = "System";
    }
}
