using GearLend.Application.Interfaces;
using GearLend.Domain.Entities;
using GearLend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GearLend.Application.Features.RentalRequests.Commands.CreateRentalRequest
{
    public class CreateRentalRequestHandler : IRequestHandler<CreateRentalRequestCommand, Guid>
    {
        private readonly IRepository<RentalRequest> _rentalRequestRepository;
        private readonly IRepository<Asset> _assetRepository;
        private readonly IBackgroundJobScheduler _backgroundJobScheduler;

        public CreateRentalRequestHandler(
            IRepository<RentalRequest> rentalRequestRepository,
            IRepository<Asset> assetRepository,
            IBackgroundJobScheduler backgroundJobScheduler)
        {
            _rentalRequestRepository = rentalRequestRepository;
            _assetRepository = assetRepository;
            _backgroundJobScheduler = backgroundJobScheduler;
        }

        public async Task<Guid> Handle(CreateRentalRequestCommand request, CancellationToken cancellationToken)
        {
            if (request.EndDate <= request.StartDate)
            {
                throw new ArgumentException("EndDate must be greater than StartDate.");
            }

            // Verify if target asset exists in database
            var asset = await _assetRepository.Get()
                .FirstOrDefaultAsync(a => a.Id == request.AssetId, cancellationToken);

            if (asset == null)
            {
                throw new KeyNotFoundException($"Asset with ID '{request.AssetId}' does not exist in the system.");
            }

            var rentalRequest = new RentalRequest
            {
                Id = Guid.NewGuid(),
                AssetId = request.AssetId,
                UserId = request.UserId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = RentalStatus.Pending,
                RequestedAt = DateTime.UtcNow
            };

            await _rentalRequestRepository.AddAsync(rentalRequest);
            await _rentalRequestRepository.SaveChangesAsync();

            // Schedule background notification job via Hangfire
            _backgroundJobScheduler.Enqueue<INotificationService>(service =>
                service.SendRentalRequestCreatedNotificationAsync(rentalRequest.Id));

            return rentalRequest.Id;
        }
    }
}
