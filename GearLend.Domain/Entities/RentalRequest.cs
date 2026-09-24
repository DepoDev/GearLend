using GearLend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearLend.Domain.Entities
{
    public class RentalRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int AssetId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RentalStatus Status { get; set; } = RentalStatus.Pending;
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime ProcessedAt { get; set; }

        // Navigation property
        public Asset? Asset { get; set; }
    }
}

