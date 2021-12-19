using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    public interface INoveltyStore : IStoreByBuilding<Novelty>
    {
        PaginationDataType<Novelty> GetByTenant(int Skip, int Take, Guid TenantTd, [Optional] string[] Relations);
    }
}
