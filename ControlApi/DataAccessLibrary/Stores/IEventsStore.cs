using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    public interface IEventsStore : IStoreByBuilding<Event>
    {
        IEnumerable<Event> GetAll(Guid BuildingId);
    }
}
