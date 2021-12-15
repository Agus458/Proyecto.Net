using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    public interface IFacturaStore : IStore<Factura>
    {
       PaginationDataType<Factura> GetAll(int Skip, int Take, Guid TenantId);
    }
}
