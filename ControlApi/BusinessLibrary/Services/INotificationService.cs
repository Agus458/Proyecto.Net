using SharedLibrary.DataTypes;
using SharedLibrary.DataTypes.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface INotificationService
    {
        Task SendNotification(string Message, Guid BuildingId);

        Task<PaginationDataType<NotificationDataType>> GetAll(int Skip, int Take);
    }
}
