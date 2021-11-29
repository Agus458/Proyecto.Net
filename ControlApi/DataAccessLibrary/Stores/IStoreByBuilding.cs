using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    public interface IStoreByBuilding<Target> : IStore<Target> where Target : MustHaveBuildingEntity
    {
        PaginationDataType<Target> GetAll(int Skip, int Take, Guid BuildingId);
    }
}
