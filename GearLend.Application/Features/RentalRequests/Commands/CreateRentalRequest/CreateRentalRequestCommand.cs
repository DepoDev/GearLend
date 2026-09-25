using MediatR;
using System;

namespace GearLend.Application.Features.RentalRequests.Commands.CreateRentalRequest
{
    public class CreateRentalRequestCommand : IRequest<Guid>
    {
        public Guid AssetId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
