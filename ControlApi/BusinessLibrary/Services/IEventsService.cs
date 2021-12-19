using SharedLibrary.DataTypes;
using SharedLibrary.DataTypes.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface IEventsService
    {
        IEnumerable<EventDataType> GetAll(Guid RoomId);

        IEnumerable<EventDataType> GetByTenant(Guid TenantId);

        EventDataType GetById(Guid Id, Guid RoomId);

        EventDataType Create(CreateEventRequestDataType Data);

        void Delete(Guid Id, Guid RoomId);

        void Update(Guid Id, UpdateEventRequestDataType Data, Guid RoomId);
    }
}
