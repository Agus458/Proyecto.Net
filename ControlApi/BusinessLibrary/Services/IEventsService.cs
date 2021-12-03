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
        IEnumerable<EventDataType> GetAll(Guid BuildingId);

        EventDataType GetById(Guid Id, Guid BuildingId);

        EventDataType Create(CreateEventRequestDataType Data);

        void Delete(Guid Id, Guid BuildingId);

        void Update(Guid Id, UpdateEventRequestDataType Data, Guid BuildingId);
    }
}
