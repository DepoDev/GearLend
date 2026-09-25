using GearLend.Application.DTOs;
using GearLend.Application.Interfaces;
using GearLend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GearLend.Application.Features.RentalRequests.Queries.GetAllRentalRequests
{
    public class GetAllRentalRequestsHandler : IRequestHandler<GetAllRentalRequestsQuery, List<RentalRequestDto>>
    {
        private readonly IRepository<RentalRequest> _rentalRequestRepository;

        public GetAllRentalRequestsHandler(IRepository<RentalRequest> rentalRequestRepository)
        {
            _rentalRequestRepository = rentalRequestRepository;
        }

        public async Task<List<RentalRequestDto>> Handle(GetAllRentalRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _rentalRequestRepository.Get()
                .Include(r => r.Asset)
                .Select(r => new RentalRequestDto
                {
                    Id = r.Id,
                    AssetId = r.AssetId,
                    AssetName = r.Asset != null ? r.Asset.Name : null,
                    UserId = r.UserId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Status = r.Status,
                    RequestedAt = r.RequestedAt,
                    ProcessedAt = r.ProcessedAt
                })
                .ToListAsync(cancellationToken);

            return requests;
        }
    }
}
