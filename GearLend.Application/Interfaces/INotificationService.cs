using System;
using System.Threading.Tasks;

namespace GearLend.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendRentalRequestCreatedNotificationAsync(Guid rentalRequestId);
        Task SendRentalStatusChangedNotificationAsync(Guid rentalRequestId, string status);
    }
}
