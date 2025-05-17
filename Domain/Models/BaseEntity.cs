using Newtonsoft.Json;

namespace SurfTicket.Domain.Models
{
    public class BaseEntity : EntityAudit
    {
        public int Id { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
    }

    public class EntityAudit
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
