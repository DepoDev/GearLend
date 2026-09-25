using GearLend.Domain.Enums;
using System;

namespace GearLend.Application.DTOs
{
    public class RentalRequestDto
    {
        public Guid Id { get; set; }
        public Guid AssetId { get; set; }
        public string? AssetName { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RentalStatus Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }
}
