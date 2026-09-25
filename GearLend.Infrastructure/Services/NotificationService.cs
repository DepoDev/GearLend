using GearLend.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GearLend.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public Task SendRentalRequestCreatedNotificationAsync(Guid rentalRequestId)
        {
            _logger.LogInformation("NOTIFICATION: Rental request {RentalRequestId} was successfully created.", rentalRequestId);
            return Task.CompletedTask;
        }

        public Task SendRentalStatusChangedNotificationAsync(Guid rentalRequestId, string status)
        {
            _logger.LogInformation("NOTIFICATION: Rental request {RentalRequestId} status changed to {Status}.", rentalRequestId, status);
            return Task.CompletedTask;
        }
    }
}
