using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    public interface IStoreByBuilding<Target> : IStore<Target> where Target : MustHaveBuildingEntity
    {
        PaginationDataType<Target> GetAll(int Skip, int Take, Guid BuildingId, [Optional] string[] Relations);

        Target GetById(Guid Id, Guid BuildingId, [Optional] string[] Relations);
    }
}
