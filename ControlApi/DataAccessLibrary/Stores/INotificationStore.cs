using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    public interface INotificationStore : IStore<Notification>
    {
        PaginationDataType<Notification> GetAll(int Skip, int Take, string UserId, [Optional] string[] Relations);

        Notification GetById(Guid Id, string UserId, [Optional] string[] Relations);

        void Clear(string UserId);
    }
}
