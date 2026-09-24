using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GearLend.Domain.Entities
{
    public class Asset
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Category { get; set; }
        public string SerialNumber { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation property
        public ICollection<RentalRequest> RentalRequests { get; set; } = new List<RentalRequest>();
    }
}
