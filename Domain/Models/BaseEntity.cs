﻿using Newtonsoft.Json;

namespace SurfTicket.Domain.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
    }
}
