using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    public interface IRoomsStore : IStore<Room>
    {
        PaginationDataType<Room> GetAll(int Skip, int Take, Guid BuildingId);
    
    }
}
