using GearLend.Application.DTOs;
using GearLend.Application.Interfaces;
using GearLend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GearLend.Application.Features.RentalRequests.Queries.GetRentalRequestById
{
    public class GetRentalRequestByIdHandler : IRequestHandler<GetRentalRequestByIdQuery, RentalRequestDto?>
    {
        private readonly IRepository<RentalRequest> _rentalRequestRepository;

        public GetRentalRequestByIdHandler(IRepository<RentalRequest> rentalRequestRepository)
        {
            _rentalRequestRepository = rentalRequestRepository;
        }

        public async Task<RentalRequestDto?> Handle(GetRentalRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var rentalRequest = await _rentalRequestRepository.Get()
                .Include(r => r.Asset)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (rentalRequest == null) return null;

            return new RentalRequestDto
            {
                Id = rentalRequest.Id,
                AssetId = rentalRequest.AssetId,
                AssetName = rentalRequest.Asset != null ? rentalRequest.Asset.Name : null,
                UserId = rentalRequest.UserId,
                StartDate = rentalRequest.StartDate,
                EndDate = rentalRequest.EndDate,
                Status = rentalRequest.Status,
                RequestedAt = rentalRequest.RequestedAt,
                ProcessedAt = rentalRequest.ProcessedAt
            };
        }
    }
}
