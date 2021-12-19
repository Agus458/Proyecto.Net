using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    public interface IEventsStore : IStore<Event>
    {
        IEnumerable<Event> GetAll(Guid RoomId);

        IEnumerable<Event> GetByTenant(Guid TenantId);
    }
}
