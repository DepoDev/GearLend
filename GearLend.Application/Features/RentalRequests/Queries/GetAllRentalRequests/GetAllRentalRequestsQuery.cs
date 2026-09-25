using GearLend.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace GearLend.Application.Features.RentalRequests.Queries.GetAllRentalRequests
{
    public class GetAllRentalRequestsQuery : IRequest<List<RentalRequestDto>>
    {
    }
}
