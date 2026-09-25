using GearLend.Application.DTOs;
using MediatR;
using System;

namespace GearLend.Application.Features.RentalRequests.Queries.GetRentalRequestById
{
    public class GetRentalRequestByIdQuery : IRequest<RentalRequestDto?>
    {
        public Guid Id { get; set; }
    }
}
