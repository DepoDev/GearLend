using System;

namespace GearLend.Application.DTOs
{
    public class CreateRentalRequestDto
    {
        public Guid AssetId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
